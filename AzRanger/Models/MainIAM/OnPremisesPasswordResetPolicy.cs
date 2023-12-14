using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MainIAM
{
    public class OnPremisesPasswordResetPolicy
    {
        public object objectId { get; set; }
        public bool passwordWritebackSupported { get; set; }
        public bool accountUnlockSupported { get; set; }
        public bool accountUnlockEnabled { get; set; }
        public bool enablementForTenant { get; set; }
        public bool cloudProvisioningEnablementForTenant { get; set; }
    }

}
