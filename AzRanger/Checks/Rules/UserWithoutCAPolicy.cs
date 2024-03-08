using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class UserWithoutCAPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            
            return CheckResult.NoFinding;
        }
    }
}
