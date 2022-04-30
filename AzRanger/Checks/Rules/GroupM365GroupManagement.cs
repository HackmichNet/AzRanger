using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("GroupM365GroupManagement", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [RuleScore("All member can create Microsoft 365 Groups", "This may result in unwanted groups configurations or group memberships", 1)]
    internal class GroupM365GroupManagement : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (EnterpriseApplicationUserSettings setting in tenant.EnterpriseApplicationUserSettings)
            {
                if (setting.displayName == "Group.Unified")
                {
                    foreach (Value value in setting.values)
                    {
                        if (value.name == "EnableGroupCreation")
                        {
                            if (value.value == "false")
                            {
                                return CheckResult.Passed;
                            }
                        }
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
