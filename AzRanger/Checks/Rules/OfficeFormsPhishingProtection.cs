using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("OfficeFormsPhishingProtection", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/OfficeForms")]
    [RuleScore("Phishing protection for Microsoft Forms is not enabled", "Microsoft can scan MS Forms for known phishing questions", 2, "https://support.microsoft.com/en-us/office/microsoft-forms-and-proactive-phishing-prevention-b3950a20-296d-4e8e-96f5-594ced998a90")]
    class OfficeFormsPhishingProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AdminCenterSettings.OfficeFormsSettings.InOrgFormsPhishingScanEnabled == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
