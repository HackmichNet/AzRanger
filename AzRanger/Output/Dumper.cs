using AzRanger.Models;
using System.Text.Json;
using System.IO;


namespace AzRanger.Output
{
    public static class Dumper
    {
        public static void DumpTenant(Tenant tenant, string outFile)
        {
           
            using (StreamWriter file = File.CreateText(outFile))
            {
                var options = new JsonSerializerOptions
                {
                    MaxDepth = 16,
                    IncludeFields = true,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(tenant, options);
                file.Write(json);
            }
        }

        public static void DumpTenantSettings(M365Settings tenant, string outFile)
        {
            using (StreamWriter file = File.CreateText(outFile))
            {
                var options = new JsonSerializerOptions
                {
                    MaxDepth = 16,
                    IncludeFields = true,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(tenant, options);
                file.Write(json);
            }
        }
    }
}
