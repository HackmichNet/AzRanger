using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllAdminsHaveMFA", ScopeEnum.AAD, MaturityLevel.Mature, null)]
    [CISM365("1.1.2", "", Level.L1, "v2.0")]
    [RuleInfo("Not all priviledged accounts uses MFA", "This increases the risk, that one of your admins becomes a victim of a phishing attack.", 10, "https://docs.microsoft.com/en-us/azure/active-directory/conditional-access/howto-conditional-access-policy-admin-mfa", null, "No discussion here. Force them to use MFA!")]
    class UserAllAdminsHaveMFA : BaseCheck
    {
        private readonly String[] InteresstingRoles = new String[] {
                "Application administrator",
                "Authentication administrator",
                "Cloud application administrator",
                "Conditional Access administrator",
                "Global Administrator",
                "Billing Administrator",
                "Exchange Administrator",
                "SharePoint Administrator",
                "Password Administrator",
                "Skype for Business Administrator",
                "User Administrator",
                "Dynamics 365 ServiceEnum Administrator",
                "Power BI Administrator",
                "Global reader",
                "Helpdesk administrator",
                "Privileged authentication administrator",
                "Privileged role administrator",
                "Security administrator",
                "User administrator"
        };

        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values.ToList())
            {
                if (InteresstingRoles.Any(x=>x == role.displayName) )
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
