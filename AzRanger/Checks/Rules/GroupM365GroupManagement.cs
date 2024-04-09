using AzRanger.Models;
using AzRanger.Models.MSGraph;

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
                            if (value.value.ToLower() == "false")
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
