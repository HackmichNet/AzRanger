using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.PasswordReset
{
    public class AdminPasswordResetPolicy
    {
        public string[] ssprEnabledAuthenticationMethods { get; set; }
        public bool enabled { get; set; }
        public int methodsRequiredToReset { get; set; }
    }

}
