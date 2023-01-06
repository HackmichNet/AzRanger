using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("ADPasswordProtection", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/PasswordProtection")]
    [CISM365("1.1.5", "", Level.L1, "v1.5")]
    [RuleInfo("Azure Active Directory Password Protection is not enabled", "Azure Active Directory Password Protection protects user using weak or leaked passwords.", 3, null, null, @"To enabled Azure Active Directory Password protection go to <a href=""https://portal.azure.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection"">https://portal.azure.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection</a> and enable ""Enable password protection on Windows Server Active Directory"".")]
    class ADPasswordProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordPolicy.enableBannedPasswordCheckOnPremises == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
