using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("TeamsExternalCommunication", Scope.O365, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/external-communications")]
    class TeamsExternalCommunication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowPublicUsers == false && tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}