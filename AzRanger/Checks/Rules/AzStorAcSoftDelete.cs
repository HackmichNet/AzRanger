using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzStorAcSoftDelete", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Storage%2FStorageAccounts", Service.StorageAccount)]
    [RuleScore("For this Storage Accounts is soft delete disabled.", "When soft delete is disabled, data cannot be recovered if they are accidentially deleted", 1)]
    internal class AzStorAcSoftDelete : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if(account.properties.networkAcls.defaultAction == "Deny")
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
            }
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
