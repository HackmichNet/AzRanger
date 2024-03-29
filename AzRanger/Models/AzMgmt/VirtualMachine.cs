﻿using AzRanger.Output;
using System;

namespace AzRanger.Models.AzMgmt
{
    public class VirtualMachine : IReporting
    {
        public string name { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public VirtualMachineIdentity identity { get; set; }
        public VirtualMachineProperties properties { get; set; }
        public VirtualMachineResource[] resources { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.name);
        }
    }

    public class VirtualMachineIdentity
    {
        public string type { get; set; }
        public string principalId { get; set; }
        public string tenantId { get; set; }
    }

    public class VirtualMachineProperties
    {
        public string vmId { get; set; }
        public VirtualMachineHardwareprofile hardwareProfile { get; set; }
        public VirtualMachineStorageprofile storageProfile { get; set; }
        public VirtualMachineOsprofile osProfile { get; set; }
        public VirtualMachineNetworkprofile networkProfile { get; set; }
        public VirtualMachineDiagnosticsprofile diagnosticsProfile { get; set; }
        public string provisioningState { get; set; }
        public DateTime timeCreated { get; set; }
    }

    public class VirtualMachineHardwareprofile
    {
        public string vmSize { get; set; }
    }

    public class VirtualMachineStorageprofile
    {
        public VirtualMachineImagereference imageReference { get; set; }
        public VirtualMachineOsdisk osDisk { get; set; }
        public object[] dataDisks { get; set; }
    }

    public class VirtualMachineImagereference
    {
        public string publisher { get; set; }
        public string offer { get; set; }
        public string sku { get; set; }
        public string version { get; set; }
        public string exactVersion { get; set; }
    }

    public class VirtualMachineOsdisk
    {
        public string osType { get; set; }
        public string name { get; set; }
        public string createOption { get; set; }
        public VirtualMachineVhd vhd { get; set; }
        public string caching { get; set; }
        public int diskSizeGB { get; set; }
        public VirtualMachineManageddisk managedDisk { get; set; }
    }

    public class VirtualMachineVhd
    {
        public string uri { get; set; }
    }
    public class VirtualMachineManageddisk
    {
        public string id { get; set; }
    }

    public class VirtualMachineOsprofile
    {
        public string computerName { get; set; }
        public string adminUsername { get; set; }
        public VirtualMachineWindowsconfiguration windowsConfiguration { get; set; }
        public object[] secrets { get; set; }
    }

    public class VirtualMachineWindowsconfiguration
    {
        public bool provisionVMAgent { get; set; }
        public bool enableAutomaticUpdates { get; set; }
        public VirtualMachinePatchsettings patchSettings { get; set; }
    }

    public class VirtualMachinePatchsettings
    {
        public string patchMode { get; set; }
        public string assessmentMode { get; set; }
    }

    public class VirtualMachineNetworkprofile
    {
        public VirtualMachineNetworkinterface[] networkInterfaces { get; set; }
    }

    public class VirtualMachineNetworkinterface
    {
        public string id { get; set; }
    }

    public class VirtualMachineDiagnosticsprofile
    {
        public VirtualMachineBootdiagnostics bootDiagnostics { get; set; }
    }

    public class VirtualMachineBootdiagnostics
    {
        public bool enabled { get; set; }
        public string storageUri { get; set; }
    }

    public class VirtualMachineResource
    {
        public string id { get; set; }
    }

}
