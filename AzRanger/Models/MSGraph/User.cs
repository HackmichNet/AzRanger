﻿using AzRanger.Models.WinGraph;
using System;

namespace AzRanger.Models.MSGraph
{
    public class User : IReporting
    {
        public Guid id { get; set; }
        public String userPrincipalName { get; set; }
        public String displayName { get; set; }
        public String userType { get; set; }
        public DateTime? createdDateTime { get; set; }
        public bool accountEnabled { get; set; }
        public Signinactivity signInActivity { get; set; }

        // MFA is registert
        public bool isMFAEnabled = false;

        // isRegistered => is Registered
        public bool isAbleTodoPasswordReset { get; set; }
        public object onPremisesSyncEnabled { get; set; }
        public StrongAuthenticationDetailDetails strongAuthenticationDetail { get; set; }

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
