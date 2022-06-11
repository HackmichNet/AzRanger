using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzStorAcAllowTrustedServices", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", Service.StorageAccount)]
    [RuleScore("Storage Account block network access for Trusted Microsft Services", "This may prevent functions like Azure Backup or Azure Monitoring from correct working", 1)]
    internal class AzStorAcAllowTrustedServices : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if(account.properties.networkAcls.defaultAction == "Deny")
                    {
                        if(account.properties.networkAcls.bypass != "AzureServices")
                        {
                            passed = false;
                            this.AddAffectedEntity(account);
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
