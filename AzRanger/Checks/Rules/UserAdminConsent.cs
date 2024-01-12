using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAdminConsent", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/ConsentPoliciesMenuBlade/~/AdminConsentSettings")]    
    class UserAdminConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(EnterpriseApplicationUserSettings setting in tenant.EnterpriseApplicationUserSettings)
            {
                if(setting.displayName == "Consent Policy Settings")
                {
                    foreach(Value value in setting.values)
                    {
                        if(value.name == "EnableAdminConsentRequests")
                        {
                            if(value.value == "true")
                            {
                                return CheckResult.NoFinding;
                            }
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
