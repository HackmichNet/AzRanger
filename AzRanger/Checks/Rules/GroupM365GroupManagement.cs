using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    internal class GroupM365GroupManagement : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (EnterpriseApplicationUserSettings setting in tenant.EnterpriseApplicationUserSettings)
            {
                if (setting.displayName == "Group.Unified")
                {
                    foreach (Value value in setting.values)
                    {
                        if (value.name == "EnableGroupCreation")
                        {
                            if (value.value == "false")
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
