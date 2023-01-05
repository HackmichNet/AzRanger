using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserCanAddGalleryApps", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/UserSettings")]
    [CISAZ("1.10", "", Level.L2, "v1.4")]
    [RuleInfo("User can add arbitrtary apps to 'My Apps'", "When a user adds an App within your tenant it shares internal information with the tenant.", 1, null, null, @"Go to the Portal Link and set ""Set Users can add gallery apps to My Apps"" to ""No"".")]
    internal class UserCanAddGalleryApps : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.UserSettings.usersCanAddGalleryApps != null && tenant.TenantSettings.UserSettings.usersCanAddGalleryApps == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
                
        }
    }
}
