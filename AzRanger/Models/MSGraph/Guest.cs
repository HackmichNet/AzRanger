using AzRanger.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Guest : IReporting
    {
        public string userPrincipalName { get; set; }
        public string displayName { get; set; }
        public string userType { get; set; }
        public string externalUserState { get; set; }
        public DateTime externalUserStateChangeDateTime { get; set; }
        public DateTime createdDateTime { get; set; }
        public string creationType { get; set; }
        public bool accountEnabled { get; set; }
        public string id { get; set; }
        public Signinactivity signInActivity { get; set; }

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
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.userPrincipalName);
        }
    }
}
