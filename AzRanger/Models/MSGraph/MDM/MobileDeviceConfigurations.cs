using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    public class MobileDeviceConfigurations
    {
        private readonly List<AndroidWorkProfileGeneralDeviceConfiguration> androidWorkProfileGeneralDeviceConfigurations;
        private readonly List<AndroidDeviceOwnerGeneralDeviceConfiguration> androidDeviceOwnerGeneralDeviceConfigurations;
        private readonly List<IosGeneralDeviceConfiguration> iosGeneralDeviceConfigurations;
        private readonly List<MacOSGeneralDeviceConfiguration> macOSGeneralDeviceConfigurations;

        public MobileDeviceConfigurations(List<AndroidWorkProfileGeneralDeviceConfiguration> androidWorkProfileGeneralDeviceConfigurations, List<AndroidDeviceOwnerGeneralDeviceConfiguration> androidDeviceOwnerGeneralDeviceConfiguration, List<IosGeneralDeviceConfiguration> iosGeneralDeviceConfigurations, List<MacOSGeneralDeviceConfiguration> macOSGeneralDeviceConfigurations)
        {
            this.androidWorkProfileGeneralDeviceConfigurations = androidWorkProfileGeneralDeviceConfigurations;
            this.androidDeviceOwnerGeneralDeviceConfigurations = androidDeviceOwnerGeneralDeviceConfiguration;
            this.iosGeneralDeviceConfigurations = iosGeneralDeviceConfigurations;
            this.macOSGeneralDeviceConfigurations = macOSGeneralDeviceConfigurations;
        }
        public List<AndroidDeviceOwnerGeneralDeviceConfiguration> GetAndroidDeviceOwnerGeneralDeviceConfigurations()
        {
            return androidDeviceOwnerGeneralDeviceConfigurations;
        }

        public List<AndroidWorkProfileGeneralDeviceConfiguration> GetAndroidWorkProfileGeneralDeviceConfiguration()
        {
            return androidWorkProfileGeneralDeviceConfigurations;
        }

        public List<IosGeneralDeviceConfiguration> GetIosGeneralDeviceConfigurations()
        {
            return iosGeneralDeviceConfigurations;
        }

        public List<MacOSGeneralDeviceConfiguration> GetMacOSGeneralDeviceConfigurations()
        {
            return macOSGeneralDeviceConfigurations;
        }
    }
}
