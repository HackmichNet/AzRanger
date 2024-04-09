using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzVirtMManagedDiskUsage : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {

                foreach (VirtualMachine machine in sub.Resources.VirtualMachines)
                {
                    if (machine.properties.storageProfile.osDisk.managedDisk == null)
                    {
                        passed = false;
                        this.AddAffectedEntity(machine);
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
