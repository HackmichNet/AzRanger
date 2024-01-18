using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    // Credits to https://github.com/soteria-security/365Inspect/blob/main/Inspectors/BypassingSafeAttachments.ps1    
    class EXOBypassSafeAttachments : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                if(rule.State != "Enabled")
                {
                    continue;
                }
                if(rule.SetHeaderName != null && (string)rule.SetHeaderName.ToString() == "X-MS-Exchange-Organization-SkipSafeAttachmentProcessing")
                {
                    passed = false;
                    this.AddAffectedEntity(rule);
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
