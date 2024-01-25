﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzStorAcSoftDelete : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {  
                    if(account.Default.properties.deleteRetentionPolicy.enabled == false || 
                        account.Default.properties.containerDeleteRetentionPolicy.enabled == false ||
                        account.Default.properties.deleteRetentionPolicy.days == 0 ||
                        account.Default.properties.containerDeleteRetentionPolicy.days == 0
                        )
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
