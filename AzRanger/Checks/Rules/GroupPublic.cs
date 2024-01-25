using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class GroupPublic : BaseCheck
    {
       
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Group group in tenant.AllGroups.Values.ToList())
            {
                if((group.visibility != null && (string)group.visibility.ToString().ToLower() == "public") & (group.securityEnabled))
                {
                    passed = false;
                    this.AddAffectedEntity(group);
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
