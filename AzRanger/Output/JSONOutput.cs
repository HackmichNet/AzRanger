using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Models.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AzRanger.Output
{
    public static class JSONOutput
    {
        public static void Print(Auditor auditor, string outFile)
        {
            List<ResultJSONItem> NoFindingList = new List<ResultJSONItem>();
            List<ResultJSONItem> ErrorList = new List<ResultJSONItem>();
            List<ResultJSONItem> FindingList = new List<ResultJSONItem>();
            
            foreach(BaseCheck check in auditor.Finding)
            {
                ResultJSONItem item = new ResultJSONItem();
                RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                RuleMetaAttribute ruleMeta = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                if (ruleInfo != null)
                {
                    item.ShortDescription = ruleInfo.ShortDescription;
                    item.RefernceLink = ruleInfo.RefernceLink;
                    item.Risk = ruleInfo.Risk;
                    item.RiskScore = ruleInfo.RiskScore;
                    item.Solution = ruleInfo.Solution;
                    item.LongDescription = ruleInfo.LongDescription;
                }

                if(ruleMeta != null)
                {
                    item.ShortName = ruleMeta.ShortName;
                    item.PortalUrl = ruleMeta.PortalUrl;
                    item.Service = ruleMeta.Service.ToString();
                    item.Scope = ruleMeta.Scope.ToString();
                    item.MaturityLevel = ruleMeta.MaturityLevel.ToString();
                }

                if(cisM365Rule != null)
                {
                    item.Version = cisM365Rule.Version;
                    item.Section = cisM365Rule.Section;
                    item.Level = cisM365Rule.Level.ToString();
                    item.CISDocument = "CIS M365";
                }

                if (cisAzRule != null)
                {
                    item.Version = cisAzRule.Version;
                    item.Section = cisAzRule.Section;
                    item.Level = cisAzRule.Level.ToString();
                    item.CISDocument = "CIS Azure";
                }

                if (check.GetAffectedEntity().Count > 0)
                {
                    List<String> affectedItems = new List<string>();
                    foreach (IReporting entity in check.GetAffectedEntity())
                    {
                        affectedItems.Add(entity.PrintConsole());
                    }
                    item.AffedtedItems = affectedItems;
                }
                FindingList.Add(item);
            }

            foreach (BaseCheck check in auditor.NoFinding)
            {
                ResultJSONItem item = new ResultJSONItem();
                RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                RuleMetaAttribute ruleMeta = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                if (ruleInfo != null)
                {
                    item.ShortDescription = ruleInfo.ShortDescription;
                    item.RefernceLink = ruleInfo.RefernceLink;
                    item.Risk = ruleInfo.Risk;
                    item.RiskScore = ruleInfo.RiskScore;
                    item.Solution = ruleInfo.Solution;
                    item.LongDescription = ruleInfo.LongDescription;
                }

                if (ruleMeta != null)
                {
                    item.ShortName = ruleMeta.ShortName;
                    item.PortalUrl = ruleMeta.PortalUrl;
                    item.Service = ruleMeta.Service.ToString();
                    item.Scope = ruleMeta.Scope.ToString();
                    item.MaturityLevel = ruleMeta.MaturityLevel.ToString();
                }

                if (cisM365Rule != null)
                {
                    item.Version = cisM365Rule.Version;
                    item.Section = cisM365Rule.Section;
                    item.Level = cisM365Rule.Level.ToString();
                    item.CISDocument = "CIS M365";
                }

                if (cisAzRule != null)
                {
                    item.Version = cisAzRule.Version;
                    item.Section = cisAzRule.Section;
                    item.Level = cisAzRule.Level.ToString();
                    item.CISDocument = "CIS Azure";
                }

                if(check.GetAffectedEntity().Count > 0)
                {
                    List<String> affectedItems = new List<string>();
                    foreach(IReporting entity in check.GetAffectedEntity())
                    {
                        affectedItems.Add(entity.PrintConsole());
                    }
                    item.AffedtedItems = affectedItems;
                }
                NoFindingList.Add(item);
            }

            foreach (BaseCheck check in auditor.Error)
            {
                ResultJSONItem item = new ResultJSONItem();
                RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                RuleMetaAttribute ruleMeta = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                if (ruleInfo != null)
                {
                    item.ShortDescription = ruleInfo.ShortDescription;
                    item.RefernceLink = ruleInfo.RefernceLink;
                    item.Risk = ruleInfo.Risk;
                    item.RiskScore = ruleInfo.RiskScore;
                    item.Solution = ruleInfo.Solution;
                    item.LongDescription = ruleInfo.LongDescription;
                }

                if (ruleMeta != null)
                {
                    item.ShortName = ruleMeta.ShortName;
                    item.PortalUrl = ruleMeta.PortalUrl;
                    item.Service = ruleMeta.Service.ToString();
                    item.Scope = ruleMeta.Scope.ToString();
                    item.MaturityLevel = ruleMeta.MaturityLevel.ToString();
                }

                if (cisM365Rule != null)
                {
                    item.Version = cisM365Rule.Version;
                    item.Section = cisM365Rule.Section;
                    item.Level = cisM365Rule.Level.ToString();
                    item.CISDocument = "CIS M365";
                }

                if (cisAzRule != null)
                {
                    item.Version = cisAzRule.Version;
                    item.Section = cisAzRule.Section;
                    item.Level = cisAzRule.Level.ToString();
                    item.CISDocument = "CIS Azure";
                }

                if (check.GetAffectedEntity().Count > 0)
                {
                    List<String> affectedItems = new List<string>();
                    foreach (IReporting entity in check.GetAffectedEntity())
                    {
                        affectedItems.Add(entity.PrintConsole());
                    }
                    item.AffedtedItems = affectedItems;
                }
                ErrorList.Add(item);
            }

            ResultJSONList FinalList = new ResultJSONList();
            FinalList.Finding = FindingList;
            FinalList.NoFinding = NoFindingList;
            FinalList.Error = ErrorList;

            using (StreamWriter file = File.CreateText(outFile))
            {
                var options = new JsonSerializerOptions
                {
                    MaxDepth = 16,
                    IncludeFields = true,
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(FinalList, options);
                file.Write(json);
            }
        }
    }
}
