using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzStorAcSecureTransfer", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", Service.StorageAccount)]
    [RuleScore("Not all StorageAccounts require a secure connection", "You can use plaintext HTTP or SMB to connect to Storage Accounts", 1)]
    internal class AzStorAcSecureTransfer : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if(account.properties.supportsHttpsTrafficOnly == false)
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
