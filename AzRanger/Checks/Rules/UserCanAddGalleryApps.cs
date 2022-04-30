using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserCanAddGalleryApps", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/UserSettings")]
    [RuleScore("User can add arbitrtary apps to 'My Apps'", "This settings allows normal users to integrate apps into their App Galery", 1)]
    internal class UserCanAddGalleryApps : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.UserSettings.usersCanAddGalleryApps == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
                
        }
    }
}
