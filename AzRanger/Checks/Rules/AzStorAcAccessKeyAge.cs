﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    internal class AzStorAcAccessKeyAge : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    DateTime key1 = DateTime.Parse(account.properties.keyCreationTime.key1.ToString());
                    DateTime key2 = DateTime.Parse(account.properties.keyCreationTime.key2.ToString());
                    DateTime now = DateTime.Now;
                    if ((now - key1).TotalDays > 90 | (now - key2).TotalDays > 90)
                    {
                        passed = false;
                        this.AddAffectedEntity(account);
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
