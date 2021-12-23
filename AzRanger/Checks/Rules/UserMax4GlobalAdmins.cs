using AzRanger.Models;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserMax4GlobalAdmins", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RolesAndAdministrators")]
    [RuleScore("Your organisation has more than four or less then two global admins", "Each admin rises the risk that your tenant is compromised", 10)]
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
                    if(role.GetMembers().Count >= MaxGA | role.GetMembers().Count <= MinGA)
                    {
                        return CheckResult.Failed;
                    }
                }
            }
            return CheckResult.Passed;
        }
    }
}
