﻿using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class SPOSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharePointInformation.SharePointInternalInfos.SharingCapability == 0)
            {
                return CheckResult.NoFinding;
            }
            if(tenant.SharePointInformation.SharePointInternalInfos.SharingDomainRestrictionMode != 0)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
