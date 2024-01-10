using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("TeamsExternalCommunicationInbound", ScopeEnum.Teams, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/external-communications")]
    [RuleInfo("TeamsExternalCommunicationInbound")]
    class TeamsExternalCommunicationInbound : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == true)
            {
                if (tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumerInbound == false)
                {
                    return CheckResult.NoFinding;
                }
            }
            else
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
