using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("ADPasswordProtection", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection")]
    [CISM365("1.1.10", "", Level.L1, "v2.0")]
    [RuleInfo("Entra ID Password Protection is not enabled", "Entra ID Password Protection protects user using weak or leaked passwords.", 3, null, null, @"To enabled Entra ID Directory Password protection go to <a href=""https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection"">https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection</a> and enable ""Enable password protection on Windows Server Active Directory"".")]
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
