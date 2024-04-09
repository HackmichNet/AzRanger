using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;

namespace AzRanger.Checks.Rules
{
    // TODO
    class MDMDeviceWipe : BaseCheck
    {
        // Only checks if a configuration exist. 
        public override CheckResult Audit(Tenant tenant)
        {
            bool androidPrivate = false;
            bool androidPass = false;
            bool iosPass = false;

            foreach (AndroidDeviceOwnerGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations())
            {
                if ((config.passwordSignInFailureCountBeforeFactoryReset != null && (long)config.passwordSignInFailureCountBeforeFactoryReset <= 10))
                {
                    androidPass = true;
                }
            }
            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if ((config.passwordSignInFailureCountBeforeFactoryReset != null && (int)config.passwordSignInFailureCountBeforeFactoryReset <= 10))
                {
                    androidPrivate = true;
                }
            }
            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if (config.passcodeSignInFailureCountBeforeWipe != null && (int)config.passcodeSignInFailureCountBeforeWipe <= 10)
                {
                    iosPass = true;
                }
            }

            if (androidPass & androidPrivate & iosPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;



        }
    }
}
