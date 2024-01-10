using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOOutlookAddins", ScopeEnum.EXO, MaturityLevel.Tentative, "https://outlook.office365.com/ecp/?form=eac&mkt=en-US")]
    [CISM365("2.8", "", Level.L2, "v2.0")]
    [RuleInfo("EXOOutlookAddins")]
    class EXOOutlookAddins : BaseCheck
    {
        private String Check1 = "My ReadWriteMailbox Apps";
        private String Check2 = "My Marketplace Apps";
        private String Check3 = "My ReadWriteMailbox Apps";
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(RoleAssignmentPolicy policy in tenant.ExchangeOnlineSettings.RoleAssignmentPolicies)
            {
                bool insertToAffected = false;
                foreach(String AssignedRole in policy.AssignedRoles)
                {
                    if(AssignedRole == Check1 | AssignedRole == Check2 | AssignedRole == Check3)
                    {
                        passed = true;
                        if (!insertToAffected)
                        {
                            this.AddAffectedEntity(policy);
                            insertToAffected = false;
                        }
                    }
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
