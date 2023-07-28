using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzVirtMManagedDiskUsage", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Compute%2FVirtualMachines", ServiceEnum.VirtualMachine)]
    [RuleInfo("Virtual Machine does not use Managed Disk", "The Virtual Machine does not use a Managed Disk. Managed disks are encrypted by default and are more resilient against failure.", 0, null, null, "Check if a migration to Managed Disk is possible.")]
    [CISAZ("7.2", "", Level.L1, "v2.0")]
    internal class AzVirtMManagedDiskUsage : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                
                foreach (VirtualMachine machine in sub.Resources.VirtualMachines)
                {
                    if(machine.properties.storageProfile.osDisk.managedDisk == null)
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
