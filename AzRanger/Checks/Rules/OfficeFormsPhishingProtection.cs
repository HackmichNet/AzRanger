using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeFormsPhishingProtection", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/OfficeForms")]
    [CISM365("2.10", "", Level.L1, "v2.0")]
    [RuleInfo("Phishing protection for Microsoft Forms is not enabled", "This prevents attackers using Microsoft Forms to asking your users for personal information.", 2, "https://support.microsoft.com/en-us/office/microsoft-forms-and-proactive-phishing-prevention-b3950a20-296d-4e8e-96f5-594ced998a90", null, @"Go to the Portal URL and mark ""Add internal phishing protection"".")]
    class OfficeFormsPhishingProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.AdminCenterSettings.OfficeFormsSettings.InOrgFormsPhishingScanEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
