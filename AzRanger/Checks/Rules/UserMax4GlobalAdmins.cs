using AzRanger.Models;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserMax4GlobalAdmins", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RolesAndAdministrators")]
    [CISM365("1.1.3", "", Level.L1, "v1.4")]
    [RuleInfo("Your organisation has more than four or less then two global admins", "Too fewer admins increases the risk, that you lose the control over your tenant. Too many admins increases the risk that your tenant is compromised.", 10, null, null, "Try to have only between two and for global admins. The most of the tasks can be performed using other roles.")]
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
