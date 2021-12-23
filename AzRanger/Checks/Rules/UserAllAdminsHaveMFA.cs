using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserAllAdminsHaveMFA", Scope.O365)]
    [RuleScore("Not all priviledged accounts uses MFA", "MFA is a powerfull method to defend your users against phishing or password guessing attacks", 10, "https://docs.microsoft.com/en-us/azure/active-directory/conditional-access/howto-conditional-access-policy-admin-mfa")]
    class UserAllAdminsHaveMFA : BaseCheck
    {
        private readonly String[] InteresstingRoles = new String[] {
                "Global Administrator",
                "Billing Administrator",
                "Exchange Administrator",
                "SharePoint Administrator",
                "Password Administrator",
                "Skype for Business Administrator",
                "Skype for Business Administrator",
                "User Administrator",
                "Dynamics 365 Service Administrator",
                "Power BI Administrator"};

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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
