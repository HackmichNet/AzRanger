using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    // Um auch Intune mit betrachten zu können, muss vorher die Intune PowerSHell im Tenant aktiviert werden
    // https://raw.githubusercontent.com/microsoftgraph/powershell-intune-samples/master/AdminConsent/GA_AdminConsent_Set.ps1
    // Client ID = d1ddf0e4-d672-4dae-b554-9d5bdfd93547
    class MDMScanner : MSGraphScanner
    {
        public const String DeviceConfigurations = "/beta/deviceManagement/deviceConfigurations";       //?$select=id,displayName
        public const String ConfigurationPolicies = "/beta/deviceManagement/configurationPolicies"; //?$select=id,name,description,platforms,technologies,isAssigned
        public const String DeviceConfiguration = "/beta/deviceManagement/deviceConfigurations/{0}";

        public const String DeviceCompliancePolicies = "/beta/deviceManagement/deviceCompliancePolicies"; //?$select=id,displayName;
        public const String DeviceCompliancePolicy = "/beta/deviceManagement/deviceCompliancePolicies/{0}"; //$expand=assignments

        public MDMScanner(Scanner scanner) : base(scanner)
        {
            this.ClientID = "d1ddf0e4-d672-4dae-b554-9d5bdfd93547";
        }

        public bool CheckIntunePowerShellAvailable()
        {
            // For later
            return false;
            
        }

        public Task<List<GenericCompliancePolicy>> GetGenericCompliancePolicy()
        {
            return GetAllOf<GenericCompliancePolicy>(DeviceCompliancePolicies, "$select=id,displayName");
        }

        public Task<List<GenericDeviceConfigurations>> GetGenericDeviceConfigurations()
        {
            return GetAllOf<GenericDeviceConfigurations>(DeviceConfigurations, "$select=id,displayName");
        }

        public async Task<MobileDeviceCompliancePolicies> GetMobileDeviceCompliancePolicies()
        {
            List<GenericCompliancePolicy> policies = await GetGenericCompliancePolicy();
            List<AndroidDeviceOwnerCompliancePolicy> android = new List<AndroidDeviceOwnerCompliancePolicy>();
            List<AndroidWorkProfileCompliancePolicy> androidPrivate = new List<AndroidWorkProfileCompliancePolicy>();
            List<IosCompliancePolicy> ios = new List<IosCompliancePolicy>();
            List<MacOSCompliancePolicy> mac = new List<MacOSCompliancePolicy>();

            foreach(GenericCompliancePolicy policy in policies)
            {
                if(policy.odatatype == "#microsoft.graph.androidDeviceOwnerCompliancePolicy")
                {
                    android.Add((AndroidDeviceOwnerCompliancePolicy) await Get<AndroidDeviceOwnerCompliancePolicy>(String.Format(DeviceCompliancePolicies, policy.id), "?$expand=assignments"));
                    continue;
                }
                if (policy.odatatype == "#microsoft.graph.androidWorkProfileCompliancePolicy")
                {
                    androidPrivate.Add((AndroidWorkProfileCompliancePolicy) await Get<AndroidWorkProfileCompliancePolicy>(String.Format(DeviceCompliancePolicies, policy.id), "?$expand=assignments"));
                    continue;
                }
                if (policy.odatatype == "#microsoft.graph.iosCompliancePolicy")
                {
                    ios.Add((IosCompliancePolicy) await Get<IosCompliancePolicy>(String.Format(DeviceCompliancePolicies, policy.id), "?$expand=assignments"));
                    continue;
                }
                if (policy.odatatype == "#microsoft.graph.macOSCompliancePolicy")
                {
                    mac.Add((MacOSCompliancePolicy)await Get<MacOSCompliancePolicy>(String.Format(DeviceCompliancePolicies, policy.id), "?$expand=assignments"));
                    continue;
                }
                logger.Debug("Unknown Device Policy: " + policy.odatatype);
            }
            return new MobileDeviceCompliancePolicies(androidPrivate, android, ios, mac);
        }

        public async Task<MobileDeviceConfigurations> GetMobileDeviceConfigurations()
        {
            
            List<GenericDeviceConfigurations> allConfigs = await GetAllOf<GenericDeviceConfigurations>(DeviceConfigurations, "$select=id,displayName");
            List<AndroidDeviceOwnerGeneralDeviceConfiguration> android = new List<AndroidDeviceOwnerGeneralDeviceConfiguration>();
            List<IosGeneralDeviceConfiguration> IOS = new List<IosGeneralDeviceConfiguration>();
            List<MacOSGeneralDeviceConfiguration> MAC = new List<MacOSGeneralDeviceConfiguration>();
            List<AndroidWorkProfileGeneralDeviceConfiguration> androidPrivate = new List<AndroidWorkProfileGeneralDeviceConfiguration>();

            foreach (GenericDeviceConfigurations config in allConfigs)
            {
                if(config.odatatype == "#microsoft.graph.androidDeviceOwnerGeneralDeviceConfiguration")
                {
                    android.Add((AndroidDeviceOwnerGeneralDeviceConfiguration) await Get<AndroidDeviceOwnerGeneralDeviceConfiguration>(String.Format(DeviceConfiguration, config.id), "?$expand=assignments"));
                    continue;
                }
                if (config.odatatype == "#microsoft.graph.androidWorkProfileGeneralDeviceConfiguration")
                {
                    androidPrivate.Add((AndroidWorkProfileGeneralDeviceConfiguration) await Get<AndroidWorkProfileGeneralDeviceConfiguration>(String.Format(DeviceConfiguration, config.id), "?$expand=assignments"));
                    continue;
                }
                if (config.odatatype == "#microsoft.graph.iosGeneralDeviceConfiguration")
                {
                    IOS.Add((IosGeneralDeviceConfiguration) await Get<IosGeneralDeviceConfiguration>(String.Format(DeviceConfiguration, config.id), "?$expand=assignments"));
                    continue;
                }
                if(config.odatatype == "#microsoft.graph.macOSGeneralDeviceConfiguration")
                {
                    MAC.Add((MacOSGeneralDeviceConfiguration) await Get<MacOSGeneralDeviceConfiguration>(String.Format(DeviceConfiguration, config.id), "?$expand=assignments"));
                    continue;
                }
                logger.Debug("Unkown Mobile Device Type: " + config.odatatype);
            }
            return new MobileDeviceConfigurations(androidPrivate ,android, IOS, MAC);
        }

        public Task<List<ConfigurationPolicy>> GetConfigurationPolicies()
        {
            return GetAllOf<ConfigurationPolicy>(ConfigurationPolicies, "$select=id,name,description,platforms,technologies,isAssigned");
        }
    }
}
