using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOModernAuthDisabled", ScopeEnum.SPO, MaturityLevel.Mature)]
    [RuleInfo("Modern authentication for SharePoint is disabled", "This increases the risk, that an attacker gain unauthorized access to your SharePoint instance.", 1, "https://docs.microsoft.com/en-us/powershell/module/sharepoint-online/set-spotenant?view=sharepoint-ps", null, @"You have to use the PowerShell and Set-SPOTenant to activate modern authentication.")]
    class SPOModernAuthDisabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharePointInformation.SharePointInternalInfos.OfficeClientADALDisabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
