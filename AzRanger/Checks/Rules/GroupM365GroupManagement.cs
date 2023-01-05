using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupM365GroupManagement", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [CISAZ("1.18", "", Level.L2, "v1.4")]
    [RuleInfo("All member can create Microsoft 365 Groups in Azure portals, API or PowerShell", "This may result in unwanted groups configurations or group memberships.", 1, null, null, @"Go to the Portal URL and set ""Users can create Microsoft 365 groups in Azure portals, API or PowerShell"" to ""No"".")]
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
                                return CheckResult.NoFinding;
                            }
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
