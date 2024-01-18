using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{    
    class TeamsExternalCommunicationFederated : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TenantFederationSettings.AllowFederatedUsers == false)
            {              
                 return CheckResult.NoFinding;    
            }
            else
            {
                if (tenant.TeamsSettings.TenantFederationSettings.AllowedDomains.AllowedDomain != null)
                {
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Finding;
        }
    }
}
