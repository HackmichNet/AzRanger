using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcKeyLastRotation", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.4", "", Level.L1, "v2.0")]
    [RuleInfo("StorageAccount key is too old", "If the Storage Accounts Access Keys are not regularly changed potentially compromised keys can result in long-term exploitable credential.", 1, null, null, @"Go to Storage Accounts and for each Storage Account go to ""Access Key"" and click ""Rotate Key"" for ""key1"" and ""key2"".")]
    internal class AzStorAcKeyLastRotation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    DateTime key1 = DateTime.Parse(account.properties.keyCreationTime.key1.ToString());
                    DateTime key2 = DateTime.Parse(account.properties.keyCreationTime.key2.ToString());

                    if((key1 - DateTime.Now).TotalDays > 90 | (key2 - DateTime.Now).TotalDays > 90)
                    {
                        passed = false;
                        AddAffectedEntity(account);
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
