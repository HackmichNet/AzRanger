using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.WinGraph
{
    public class StrongAuthenticationDetail
    {
        [JsonPropertyName("odata.metadata")]
        public string odatametadata { get; set; }
        [JsonPropertyName("odata.type")]
        public string odatatype { get; set; }
        public StrongAuthenticationDetailDetails strongAuthenticationDetail { get; set; }
        public Guid objectId { get; set; }
    }

    public class StrongAuthenticationDetailDetails
    {
        public object encryptedPinHash { get; set; }
        public object encryptedPinHashHistory { get; set; }
        public StrongAuthenticationDetailMethod[] methods { get; set; }
        public StrongAuthenticationDetailOathtokenmetadata[] oathTokenMetadata { get; set; }
        public object[] requirements { get; set; }
        public Phoneappdetail[] phoneAppDetails { get; set; }
        public string proofupTime { get; set; }
        public StrongAuthenticationDetailVerificationdetail verificationDetail { get; set; }
    }

    public class StrongAuthenticationDetailVerificationdetail
    {
        public object alternativePhoneNumber { get; set; }
        public object email { get; set; }
        public object voiceOnlyPhoneNumber { get; set; }
        public string phoneNumber { get; set; }
    }

    public class StrongAuthenticationDetailMethod
    {
        public string methodType { get; set; }
        public bool isDefault { get; set; }
    }

    public class StrongAuthenticationDetailOathtokenmetadata
    {
        public string id { get; set; }
        public object enabled { get; set; }
        public string tokenType { get; set; }
        public string manufacturer { get; set; }
        public object[] manufacturerProperties { get; set; }
        public string serialNumber { get; set; }
    }

    public class StrongAuthenticationDetailPhoneappdetail
    {
        public string authenticationType { get; set; }
        public object authenticatorFlavor { get; set; }
        public string deviceId { get; set; }
        public string deviceToken { get; set; }
        public string deviceName { get; set; }
        public string deviceTag { get; set; }
        public object hashFunction { get; set; }
        public string id { get; set; }
        public object lastAuthenticatedTimestamp { get; set; }
        public object oathSecretKey { get; set; }
        public int oathTokenTimeDrift { get; set; }
        public object tenantDeviceId { get; set; }
        public int timeInterval { get; set; }
        public string phoneAppVersion { get; set; }
        public string notificationType { get; set; }
    }

}
