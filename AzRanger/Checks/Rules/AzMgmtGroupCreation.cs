using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{    
    internal class AzMgmtGroupCreation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.ManagementGroupSettings == null)
            {
                return CheckResult.NotApplicable;
            }
            if(tenant.ManagementGroupSettings.properties.requireAuthorizationForGroupCreation == true )
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
