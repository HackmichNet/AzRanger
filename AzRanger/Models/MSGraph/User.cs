using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class User : IEntity
    {
        public Guid id { get; set; }
        public String userPrincipalName { get; set; }
        public String displayName { get; set; }
        public String userType { get; set; }
        public DateTime createdDateTime { get; set; }
        public bool accountEnabled { get; set; }
        public Signinactivity signInActivity { get; set; }

        // MFA is registert
        public bool isMFAEnabled = false;

        // isRegistered => is Registered
        public bool isAbleTodoPasswordReset { get; set; }

        public override string ToString()
        {
            return this.id.ToString() + ": " + this.userPrincipalName;
        }

        public string PrintConsole()
        {
            return this.userPrincipalName + " - " + this.id;
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, userPrincipalName);
        }
    }
}
