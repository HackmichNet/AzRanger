﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    internal class AzSecurityContacts : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityContact == null || sub.SecurityContact.Count == 0)
                {
                    passed = false;
                    this.AddAffectedEntity(sub);
                }
                else
                {
                    if (sub.SecurityContact.Any(x => x.properties.emails == "")) {
                        passed = false;
                        this.AddAffectedEntity(sub);
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
