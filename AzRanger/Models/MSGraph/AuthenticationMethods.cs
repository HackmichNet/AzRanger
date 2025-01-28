using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class AuthenticationMethods
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        public AuthenticationMethod[] value { get; set; }
    }

    public class AuthenticationMethod
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public string phoneNumber { get; set; }
        public string phoneType { get; set; }
        public string smsSignInState { get; set; }
        public object password { get; set; }
        public DateTime? createdDateTime { get; set; }
        public string displayName { get; set; }
        public string keyStrength { get; set; }
        public string deviceTag { get; set; }
        public string phoneAppVersion { get; set; }
    }

}
