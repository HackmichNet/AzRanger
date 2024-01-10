using AzRanger.Models;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserMax4GlobalAdmins", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RolesAndAdministrators")]
    [CISM365("1.1.7", "", Level.L1, "v2.0")]
    [RuleInfo("UserMax4GlobalAdmins")]
    class UserMax4GlobalAdmins : BaseCheck
    {
        int MaxGA = 4;
        int MinGA = 2;
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
            {
                if(role.roleTemplateId == DirectoryRoleTemplateID.GlobalAdministrator)
                {
                    if(role.GetMembers().Count > MaxGA | role.GetMembers().Count < MinGA)
                    {
                        return CheckResult.Finding;
                    }
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
