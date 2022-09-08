using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("TeamsExternalCommunication", Scope.O365, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/external-communications")]
    [CISM365("3.3", "", Level.L2, "v1.4")]
    class TeamsExternalCommunication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowPublicUsers == false && tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}