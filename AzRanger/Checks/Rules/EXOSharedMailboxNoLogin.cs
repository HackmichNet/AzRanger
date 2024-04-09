using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.MSGraph;
using System;

namespace AzRanger.Checks.Rules
{
    class EXOSharedMailboxNoLogin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool finding = false;

            foreach (Mailbox box in tenant.ExchangeOnlineSettings.Mailboxes)
            {
                if (box.IsShared)
                {
                    if(box.ExternalDirectoryObjectId != null) {
                        var success = Guid.TryParse(box.ExternalDirectoryObjectId, out Guid id);
                        if(success)
                        {
                            if (tenant.Users.ContainsKey(id))
                            {
                                User mailboxUser = tenant.Users[id];
                                if (mailboxUser != null)
                                {
                                    if (mailboxUser.accountEnabled)
                                    {
                                        AddAffectedEntity(box);
                                        finding = true;
                                    }
                                }
                            }
                        } 
                    }
                }
            }

            if(finding)
            {
                return CheckResult.Finding; ;
            }
            return CheckResult.NoFinding;
            
        }
    }
}
