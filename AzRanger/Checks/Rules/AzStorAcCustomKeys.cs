using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzStorAcCustomKeys", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", Service.StorageAccount)]
    [RuleScore("The following StorageAccounts are encrypted with Microsot managed keys", "If you want to have control over your keys, you should use custom keys", 1)]
    internal class AzStorAcCustomKeys : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if(account.properties.encryption.keySource == "Microsoft.Storage")
                    {
                        passed = false;
                        this.AddAffectedEntity(account);
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
