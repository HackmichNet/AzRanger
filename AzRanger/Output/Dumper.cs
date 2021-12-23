using AzRanger.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    public static class Dumper
    {
        public static void DumpTenant(Tenant tenant, string outFile)
        {
           
            using (StreamWriter file = File.CreateText(outFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, tenant);
            }
        }

        public static void DumpTenantSettings(TenantSettings tenant, string outFile)
        {
            using (StreamWriter file = File.CreateText(outFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, tenant);
            }
        }
    }
}
