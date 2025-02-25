using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class UserRegistrationDetail
    {
        public string id { get; set; }
        public string userPrincipalName { get; set; }
        public string userDisplayName { get; set; }
        public string userType { get; set; }
        public bool isAdmin { get; set; }
        public bool isSsprRegistered { get; set; }
        public bool isSsprEnabled { get; set; }
        public bool isSsprCapable { get; set; }
        public bool isMfaRegistered { get; set; }
        public bool isMfaCapable { get; set; }
        public bool isPasswordlessCapable { get; set; }
        public string[] methodsRegistered { get; set; }
        public bool isSystemPreferredAuthenticationMethodEnabled { get; set; }
        public string[] systemPreferredAuthenticationMethods { get; set; }
        public string userPreferredMethodForSecondaryAuthentication { get; set; }
        public DateTime lastUpdatedDateTime { get; set; }
    }

}
