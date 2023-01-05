using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcContainerPublicAccess", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.5", "", Level.L1, "v1.4")]
    [RuleInfo("StorageAccount Container with public access", "This can lead to data loss.", 1, null, null, "Ensure that only public container listed below.")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
