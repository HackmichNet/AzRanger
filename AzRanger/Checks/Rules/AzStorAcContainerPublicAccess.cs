using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzStorAcContainerPublicAccess", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", Service.StorageAccount)]
    [RuleScore("Some StorageAccount Container may allow public access", "This can lead to data loss, ensure that only public containerlisted below", 1)]
    internal class AzStorAcContainerPublicAccess : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if (account.properties.allowBlobPublicAccess == true)
                    {
                        foreach (StorageContainer container in account.StorageContainers)
                        {
                            if (container.properties.publicAccess != "None")
                            {
                                passed = false;
                                this.AddAffectedEntity(container);
                            }
                        }
                    }
                }
            }
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
