using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcSecureTransfer", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.1", "", Level.L1, "v1.4")]
    [RuleInfo("StorageAccounts does not require a secure connection", "User can use plaintext protocolls to access Storage Accounts. This can lead to potential data loss.", 1)]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
