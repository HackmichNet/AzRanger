using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzStorAcMinTLSVersion", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", ServiceEnum.StorageAccount)]
    [CISAZ("3.15", "", Level.L1, "v2.0")]
    [RuleInfo("StorageAccount does allow legacy TLS protocols", "TLS 1.0 has some known vulnerabilities. Using this legacy protocol can reduce the security of data in transit.", 1, null, null, @"For each Storage Account under ""Settings"" go to ""Configuration"" and set the ""Minimum TLS version"" to ""1.2"".")]
    internal class AzStorAcMinTLSVersion : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if(account.properties.minimumTlsVersion != "TLS1_2")
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
