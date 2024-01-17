using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AzRanger.Checks.Rules
{
    class UserConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // Seems to be null now nowadays...
            if(tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData != null && tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData == false)
            {
                return CheckResult.NoFinding;
            }
            if(tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData == null)
            {
                if(tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole != null)
                {
                    List<String> data = tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole.Select(s => (string)s.ToString()).ToList();
                    // String[] data = (String[])tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole;
                    foreach (String entry in data)
                    {
                        if (entry.Equals("ManagePermissionGrantsForSelf.microsoft-user-default-legacy"))
                        {
                            return CheckResult.Finding;
                        }
                    }
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Error;
        }
    }
}
