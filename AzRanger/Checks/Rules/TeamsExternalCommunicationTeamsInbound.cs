using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("TeamsExternalCommunicationInbound", Scope.O365, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/external-communications")]
    [RuleScore("Teams inbound communication with users outside of your organisation is allowed", "Allowing user to communicate outside the organisation increases the risk of phishing and data leakage", 7, "https://danielchronlund.com/2021/02/22/manage-teams-external-access-for-allowed-domains-using-powershell-and-teams-approvals/")]
    class TeamsExternalCommunicationInbound : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == true)
            {
                if (tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumerInbound == false)
                {
                    return CheckResult.Passed;
                }
            }
            else
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
