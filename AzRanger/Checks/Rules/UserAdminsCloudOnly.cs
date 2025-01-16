using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using NLog;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class UserAdminsCloudOnly : BaseCheck
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            foreach (DirectoryRole role in tenant.DirectoryRoles.Values.ToList())
            {
                if (Roles.Any(x => x == role.id.ToString()))
                {
                    DirectoryRole TmpRole = new DirectoryRole(role.id, role.displayName, null, null);
                    foreach (AzurePrincipal member in role.GetMembers())
                    {
                        if (member.PrincipalType == AzurePrincipalType.User)
                        {
                            if (tenant.Users.ContainsKey(member.id) && tenant.Users[member.id].onPremisesSyncEnabled != null && (bool)tenant.Users[member.id].onPremisesSyncEnabled == true)
                            {
                                this.AddAffectedEntity(tenant.Users[member.id]);
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
