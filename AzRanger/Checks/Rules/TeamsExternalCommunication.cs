using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("TeamsExternalCommunication", ScopeEnum.Teams, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/external-communications")]
    [CISM365("3.3", "", Level.L2, "v2.0")]
    // TODO
    class TeamsExternalCommunication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
             if (tenant.TeamsSettings.TenantFederationSettings.AllowPublicUsers == false && tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == false) {

                if(tenant.TeamsSettings.TenantFederationSettings.AllowFederatedUsers == false)
                {
                    return CheckResult.NoFinding;
                }
                else
                {
                    if (tenant.TeamsSettings.TenantFederationSettings.AllowedDomains.AllowedDomain != null) {
                        if(tenant.TeamsSettings.TenantFederationSettings.AllowedDomains.AllowedDomain.Length > 0)
                        {
                            return CheckResult.NoFinding;
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}