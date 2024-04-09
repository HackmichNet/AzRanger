using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzActNSGSUnrestrictedSourceAccess : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {

                foreach (NetworkSecurityGroup nsg in sub.Resources.NetworkSecurityGroups)
                {
                    foreach (NetworkSecurityGroupSecurityrule rule in nsg.properties.securityRules)
                    {
                        if (rule.properties.direction == "Inbound")
                        {
                            if (rule.properties.sourceAddressPrefix != null)
                            {

                                if (rule.properties.sourceAddressPrefix == "*" ||
                                    rule.properties.sourceAddressPrefix == "0.0.0.0" ||
                                    rule.properties.sourceAddressPrefix == "/0" ||
                                    rule.properties.sourceAddressPrefix == "internet" ||
                                    rule.properties.sourceAddressPrefix == "any" ||
                                    rule.properties.sourceAddressPrefix.EndsWith("/0"))
                                {
                                    passed = false;
                                    this.AddAffectedEntity(nsg);
                                }
                            }
                        }
                    }
                }

            }

            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
