using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserGuestWithHighPrivRole", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RolesAndAdministrators")]
    [RuleScore("These guests are member of Global Admin/Privileged Authentication Administrator role", "Because you cannot control how a guest authenticates and how the security state of a guest is, this is not recommended", 9)]
    class UserGuestWithHighPrivRole : BaseCheck
    {
        bool passed = true;
        public override CheckResult Audit(Tenant tenant)
        {
            List<DirectoryRole> globalAdminRoles = new List<DirectoryRole>();
            foreach (String highPrivRole in DirectoryRoleTemplateID.GlobalAdmins)
            {
                foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
                {
                    if (role.roleTemplateId == highPrivRole)
                    {
                        globalAdminRoles.Add(role);
                        break;
                    }
                }
            }

            foreach(DirectoryRole role in globalAdminRoles)
            {
                foreach(AzurePrincipal principal in role.GetMembers())
                {
                    if (principal.PrincipalType == AzurePrincipalType.User)
                    {
                        if(tenant.AllGuests.ContainsKey(principal.id))
                        {
                            this.AddAffectedEntity(tenant.AllGuests[principal.id]);
                            passed = false;
                        }
                    }
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
