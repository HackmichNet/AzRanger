using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcAllowTrustedServices", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.7", "", Level.L2, "v1.4")]
    [RuleInfo("Storage Account block network access for Trusted Microsft Services", "This may prevent functions like Azure Backup or Azure Monitoring from correct working.", 1, null, null, "When you are using services from Microsoft like Logging or Backup, allow the network access for these services.")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
