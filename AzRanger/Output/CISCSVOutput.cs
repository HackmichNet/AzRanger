using AzRanger.Checks;
using AzRanger.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    class CISCSVOutput
    {
        public static void Print(Auditor auditor, String outPath)
        {
            // CIS

            CsvConfiguration config = new CsvConfiguration(CultureInfo.CurrentCulture);
            config.Delimiter = ";";
            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteField("CheckIdentifier");
                csvWriter.WriteField("CIS Section");
                csvWriter.WriteField("CIS Checkname");
                csvWriter.WriteField("Scope");
                csvWriter.WriteField("Maturity");
                csvWriter.WriteField("Result");
                csvWriter.WriteField("ID");
                csvWriter.WriteField("Name");
                csvWriter.WriteField("PortalLink");
                csvWriter.NextRecord();

                foreach (BaseCheck check in auditor.Finding)
                {

                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    CISM365Attribute cisM65Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                    CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof (CISAZAttribute));
                    if (cisM65Rule != null)
                    {

                        if (check.GetAffectedEntity().Count > 0)
                        {
                            foreach (IReporting entity in check.GetAffectedEntity())
                            {
                                csvWriter.WriteField(ruleInfo.ShortName);
                                csvWriter.WriteField(cisM65Rule.Section);
                                csvWriter.WriteField(cisM65Rule.Title);
                                csvWriter.WriteField("CISM365");
                                csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                                csvWriter.WriteField("Failed");
                                String[] data = entity.PrintCSV().Split(';');
                                csvWriter.WriteField(data[0]);
                                csvWriter.WriteField(data[1]);
                                csvWriter.WriteField(ruleInfo.PortalUrl);
                                csvWriter.NextRecord();
                            }
                        }
                        else
                        {

                            csvWriter.WriteField(ruleInfo.ShortName);
                            csvWriter.WriteField(cisM65Rule.Section);
                            csvWriter.WriteField(cisM65Rule.Title);
                            csvWriter.WriteField("CISM365");
                            csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                            csvWriter.WriteField("Failed");
                            csvWriter.WriteField("");
                            csvWriter.WriteField("");
                            csvWriter.WriteField(ruleInfo.PortalUrl);
                            csvWriter.NextRecord();
                        }
                    }
                    if(cisAzRule != null)
                    {
                        if (check.GetAffectedEntity().Count > 0)
                        {
                            foreach (IReporting entity in check.GetAffectedEntity())
                            {
                                csvWriter.WriteField(ruleInfo.ShortName);
                                csvWriter.WriteField(cisAzRule.Section);
                                csvWriter.WriteField(cisAzRule.Title);
                                csvWriter.WriteField("CISM365");
                                csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                                csvWriter.WriteField("Failed");
                                String[] data = entity.PrintCSV().Split(';');
                                csvWriter.WriteField(data[0]);
                                csvWriter.WriteField(data[1]);
                                csvWriter.WriteField(ruleInfo.PortalUrl);
                                csvWriter.NextRecord();
                            }
                        }
                        else
                        {

                            csvWriter.WriteField(ruleInfo.ShortName);
                            csvWriter.WriteField(cisAzRule.Section);
                            csvWriter.WriteField(cisAzRule.Title);
                            csvWriter.WriteField("CISAZ");
                            csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                            csvWriter.WriteField("Failed");
                            csvWriter.WriteField("");
                            csvWriter.WriteField("");
                            csvWriter.WriteField(ruleInfo.PortalUrl);
                            csvWriter.NextRecord();
                        }
                    }
                }

                foreach (BaseCheck check in auditor.NoFinding)
                {
                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                    CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));
                    if (cisM365Rule != null)
                    {

                        csvWriter.WriteField(ruleInfo.ShortName);
                        csvWriter.WriteField(cisM365Rule.Section);
                        csvWriter.WriteField(cisM365Rule.Title);
                        csvWriter.WriteField("CISM365");
                        csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                        csvWriter.WriteField("Passed");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                        csvWriter.WriteField(ruleInfo.PortalUrl);
                        csvWriter.NextRecord();
                    }
                    if(cisAzRule != null)
                    {
                        csvWriter.WriteField(ruleInfo.ShortName);
                        csvWriter.WriteField(cisAzRule.Section);
                        csvWriter.WriteField(cisAzRule.Title);
                        csvWriter.WriteField("CISAZ");
                        csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                        csvWriter.WriteField("Passed");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                        csvWriter.WriteField(ruleInfo.PortalUrl);
                        csvWriter.NextRecord();
                    }
                }

                foreach (BaseCheck check in auditor.Error)
                {
                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                    CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));
                    if (cisM365Rule != null)
                    {

                        csvWriter.WriteField(ruleInfo.ShortName);
                        csvWriter.WriteField(cisM365Rule.Section);
                        csvWriter.WriteField(cisM365Rule.Title);
                        csvWriter.WriteField("CISM365");
                        csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                        csvWriter.WriteField("Error");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                        csvWriter.WriteField(ruleInfo.PortalUrl);
                        csvWriter.NextRecord();
                    }
                    if (cisAzRule != null)
                    {
                        csvWriter.WriteField(ruleInfo.ShortName);
                        csvWriter.WriteField(cisAzRule.Section);
                        csvWriter.WriteField(cisAzRule.Title);
                        csvWriter.WriteField("CISAZ");
                        csvWriter.WriteField(ruleInfo.MaturityLevel.ToString());
                        csvWriter.WriteField("Error");
                        csvWriter.WriteField("");
                        csvWriter.WriteField("");
                        csvWriter.WriteField(ruleInfo.PortalUrl);
                        csvWriter.NextRecord();
                    }
                }

                writer.Flush();
                var result = Encoding.UTF8.GetString(mem.ToArray());
                var fileDestination = outPath + "\\" + "CISResult.csv";
                File.WriteAllText(fileDestination, result);
            }
        }
    }
}
