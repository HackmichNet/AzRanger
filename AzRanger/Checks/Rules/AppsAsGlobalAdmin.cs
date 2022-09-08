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
    [RuleMeta("AppsAsGlobalAdmin", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/~/AppAppsPreview/menuId~/null")]
    [RuleInfo(@"Service principals which are member of the Roles ""Global Admin"" or ""Privileged Authentication Administrator""", @"A user with ""owner"" rights to the service principal or with the role ""Application administrator"" or ""Cloud application administrator"" can use this configuration to elevate its privileges.", 9, "https://posts.specterops.io/azure-privilege-escalation-via-service-principal-abuse-210ae2be2a5", null, @"Remove the service principals from these roles.")]
    class AppsAsGlobalAdmin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
            {
                if (DirectoryRoleTemplateID.GlobalAdmins.Any(role.roleTemplateId.Contains))
                {
                    foreach(AzurePrincipal id in role.GetMembers())
                    {
                        if(id.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            passed = false;
                            this.AddAffectedEntity(tenant.AllServicePrincipals[id.id]);
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
