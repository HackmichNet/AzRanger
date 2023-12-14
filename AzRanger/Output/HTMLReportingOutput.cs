using AzRanger.Checks;
using AzRanger.Models;
using ICSharpCode.SharpZipLib.Zip;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    internal class HTMLReportingOutput
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Print(Auditor auditor, Tenant tenant, String outPath)
        {
            if (outPath == null | outPath.Length == 0)
            {
                outPath = ".";
            }
            else
            {
                if (Directory.Exists(outPath))
                {
                    Directory.Delete(outPath, true);
                }
                Directory.CreateDirectory(outPath);
            }
            try
            {
                //ResourceManager objResMgr = new ResourceManager
                //    ("namespace.resource_filename", Assembly.GetExecutingAssembly());
                //byte[] objData = (byte[])objResMgr.GetObject("ReportTemplate");
                byte[] objData = Properties.Resource.ReportTemplate;
                MemoryStream objMS = new MemoryStream(objData);
                ZipInputStream objZIP = new ZipInputStream(objMS);
                ZipEntry theEntry;
                while ((theEntry = objZIP.GetNextEntry()) != null)
                {
                    if (theEntry.Name == "css/" | theEntry.Name == "js/")
                    {
                        Directory.CreateDirectory(Path.Combine(outPath, theEntry.Name));
                    }
                    else
                    {
                        FileStream streamWriter =
                            File.Create(Path.Combine(outPath, theEntry.Name));
                        int size = objData.Length;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = objZIP.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
                objZIP.Close();
            }
            catch (MissingManifestResourceException ex)
            {
                logger.Debug("[-] " + ex.Message);
            }
            catch (Exception e1)
            {
                logger.Debug("[-] " + e1.Message);
            }

            using (StreamWriter file = File.CreateText(Path.Combine(outPath, "js/data.js")))
            {
                var options = new JsonSerializerOptions
                {
                    MaxDepth = 16,
                    IncludeFields = true,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(tenant, options);
                file.Write("var tenantData = " + json);
            }

            using (StreamWriter file = File.CreateText(Path.Combine(outPath, "js/report.js")))
            {
                var json = JSONOutput.createJSON(auditor);
                file.Write("var reportData = " + json);
            }
        }
    }
}
