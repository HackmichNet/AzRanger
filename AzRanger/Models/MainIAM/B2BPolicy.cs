﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MainIAM
{
    public class B2BPolicy
    {
        public string[] targetedDomains { get; set; }
        public bool hasListEntries { get; set; }
        public bool isAllowlist { get; set; }
        public bool otpEnabled { get; set; }
        public object[] adminConsentedForUsersIntoTenantIds { get; set; }
        public object[] noAADConsentForUsersFromTenantsIds { get; set; }
    }

}
