using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    internal class UserCanAddGalleryApps : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.UserSettings.usersCanAddGalleryApps != null && tenant.TenantSettings.UserSettings.usersCanAddGalleryApps == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;

        }
    }
}
