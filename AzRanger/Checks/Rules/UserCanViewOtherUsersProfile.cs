using AzRanger.Models;

namespace AzRanger.Checks.Rules
{    
    class UserCanViewOtherUsersProfile : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // TODO Need to check what I thought here 
            return CheckResult.NotApplicable;
            // 0 => Self service password reset enabled = None
            //if (tenant.PasswordResetPolicies.enablementType == 0)
            //{
            //    return CheckResult.Finding;
            //}
            //return CheckResult.Passed;
        }
    }
}
