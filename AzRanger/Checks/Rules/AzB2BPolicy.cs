﻿using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class AzB2BPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // If not set it can be null
            if(tenant.TenantSettings.B2BPolicy == null)
            {
                return CheckResult.Finding;
            }
            if (tenant.TenantSettings.B2BPolicy.isAllowlist)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
