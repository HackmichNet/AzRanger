using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzKeyVLogging", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", Service.KeyVault)]
    [RuleScore("For this Key Vault is no logging active.", "Without proper logging incident response becomes much harder", 1)]
    internal class AzKeyVLogging : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(KeyVault vault in sub.Resources.KeyVaults)
                {
                    bool passedSettings = false;
                    foreach(DiagnosticSettings diagnosticSettings in vault.DiagnosticSettings)
                    {
                        foreach(DiagnosticSettingsLog log in diagnosticSettings.properties.logs)
                        {
                            if(log.category == "AuditEvent" && log.retentionPolicy.enabled && log.retentionPolicy.days < 0)
                            {
                                passedSettings = true;
                            }
                        }
                    }
                    if(passedSettings == false)
                    {
                        passed = false;
                        this.AddAffectedEntity(vault);
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
