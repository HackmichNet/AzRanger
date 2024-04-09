using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    internal class AzSecurityContactsNotifyOwner : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityContact == null)
                {
                    passed = false;
                    this.AddAffectedEntity(sub);
                }
                else
                {
                    if (sub.SecurityContact.Any(x => x.properties.notificationsByRole.state == "Off"))
                    {
                        passed = false;
                        this.AddAffectedEntity(sub);
                    }
                    else
                    {
                        bool containsOwner = false;
                        sub.SecurityContact.ForEach(x =>
                        {
                            foreach (String role in x.properties.notificationsByRole.roles)
                            {
                                if (role == "Owner")
                                {
                                    containsOwner = true;
                                }
                            }
                            if (!containsOwner)
                            {
                                passed = false;
                                this.AddAffectedEntity(sub);
                            }
                        });
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
