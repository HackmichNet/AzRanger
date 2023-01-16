using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcInfraEncrypt", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.2", "", Level.L2, "v1.5")]
    [RuleInfo("StorageAccounts dpes not require infrastructure encryption", "Encrypting data on the hardware level provides an additional protection against data loss.", 0, null, null, @"You cannot enable this for already existent Storage Accounts. You must create new one and on the tap ""Encryption"" choose the right options.")]
    internal class AzStorAcInfraEncrypt : BaseCheck
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
