using AzRanger.Models;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;

namespace AzRanger.Checks.Rules
{
    class UserMax4GlobalAdmins : BaseCheck
    {
        int MaxGA = 4;
        int MinGA = 2;
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (DirectoryRole role in tenant.DirectoryRoles.Values)
            {
                if (role.roleTemplateId == DirectoryRoleTemplateID.GlobalAdministrator)
                {
                    if (role.GetMembers().Count > MaxGA | role.GetMembers().Count < MinGA)
                    {
                        return CheckResult.Finding;
                    }
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
