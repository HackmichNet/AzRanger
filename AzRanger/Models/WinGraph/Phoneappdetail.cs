namespace AzRanger.Models.WinGraph
{
    public class Phoneappdetail
    {
        public string authenticationType { get; set; }
        public object authenticatorFlavor { get; set; }
        public object deviceId { get; set; }
        public string deviceToken { get; set; }
        public string deviceName { get; set; }
        public string deviceTag { get; set; }
        public object hashFunction { get; set; }
        public string id { get; set; }
        // DateTime
        public object lastAuthenticatedTimestamp { get; set; }
        public object oathSecretKey { get; set; }
        public int oathTokenTimeDrift { get; set; }
        public object tenantDeviceId { get; set; }
        public object timeInterval { get; set; }
        public string phoneAppVersion { get; set; }
        public string notificationType { get; set; }
    }
}
