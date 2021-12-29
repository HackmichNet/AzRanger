using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("GroupPublic", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/AllGroups")]
    [RuleScore("There exist public groups in your tenant", "Every user in your tenant can add himself into a public group and access all data associated with this group", 3, "https://www.dummies.com/software/microsoft-office/office-365-groups/")]
    class GroupPublic : BaseCheck
    {
       
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Group group in tenant.AllGroups.Values.ToList())
            {
                if((group.visibility != null && (string)group.visibility.ToString() == "Public") & (group.securityEnabled))
                {
                    passed = false;
                    this.AddAffectedEntity(group);
                }
            }
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
