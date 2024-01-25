using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class UserAllAdminsHaveMFA : BaseCheck
    {

        private readonly string[] Roles = new string[]
        {
            DirectoryRoleTemplateID.ApplicationAdministrator,
            DirectoryRoleTemplateID.AuthenticationAdministrator,
            DirectoryRoleTemplateID.CloudApplicationAdministrator,
            DirectoryRoleTemplateID.ConditionalAccessAdministrator,
            DirectoryRoleTemplateID.GlobalAdministrator,
            DirectoryRoleTemplateID.BilingAdministrator,
            DirectoryRoleTemplateID.ExchangeAdministrator,
            DirectoryRoleTemplateID.SharePointAdmin,
            DirectoryRoleTemplateID.PasswordAdministrator,
            DirectoryRoleTemplateID.SkypeAdministrator,
            DirectoryRoleTemplateID.UserAdministrator,
            DirectoryRoleTemplateID.DynamicsAdministrator,
            DirectoryRoleTemplateID.PowerPlatformAdministrator,
            DirectoryRoleTemplateID.FabricAdministrator,
            DirectoryRoleTemplateID.GlobalReader,
            DirectoryRoleTemplateID.HelpdeskAdministrator,
            DirectoryRoleTemplateID.PrivilegedAuthenticationAdministrator,
            DirectoryRoleTemplateID.PrivilegedRoleAdministrator,
            DirectoryRoleTemplateID.SecurityAdministrator,
        };

        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values.ToList())
            {
                if (Roles.Any(x=>x == role.id.ToString()) )
                {
                    DirectoryRole TmpRole = new DirectoryRole(role.id, role.displayName, null, null);
                    foreach (AzurePrincipal member in role.GetMembers())
                    {
                        if (member.PrincipalType == AzurePrincipalType.User)
                        {
                            if (tenant.AllUsers.ContainsKey(member.id) && tenant.AllUsers[member.id].isMFAEnabled == false)
                            {
                                this.AddAffectedEntity(tenant.AllUsers[member.id]);
                                passed = false;
                            }
                            // Assuming guest does not have MFA
                            if (tenant.AllGuests.ContainsKey(member.id))
                            {
                                this.AddAffectedEntity(tenant.AllGuests[member.id]);
                                passed = false;
                            }
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
