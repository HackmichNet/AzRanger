using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Models.Generic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AzRanger.Output
{
    public static class JSONOutput
    {
        public static void Print(Auditor auditor, string outPath)
        {
            if(outPath == null | outPath.Length == 0)
            {
                outPath = ".";
            }
            string outFile = Path.Combine(outPath,  "report.json");
            using (StreamWriter file = File.CreateText(outFile))
            {
                var json = createJSON(auditor);                
                file.Write(json);
            }
        }
        
        private static ResultJSONItem GetResultJSONItem(BaseCheck check)
        {
            ResultJSONItem item = new ResultJSONItem();
            if (RuleInfo.TryGet(check.GetType().Name, out RuleInfo ruleInfo))
            {
                item.ShortDescription = ruleInfo.ShortDescription;
                item.Risk = ruleInfo.Risk;
                item.ReferenceLink = ruleInfo.ReferenceLink;
                item.LongDescription = ruleInfo.LongDescription;
                item.Solution = ruleInfo.Solution;
                item.RiskScore = ruleInfo.RiskScore;
            }

            if (RuleMeta.TryGet(check.GetType().Name, out RuleMeta meta))
            {
                item.ShortName = meta.ShortName;
                item.PortalUrl = meta.PortalUrl;
                item.Service = meta.Service.ToString();
                item.Scope = meta.Scope.ToString();
                item.MaturityLevel = meta.MaturityLevel.ToString();
            }

            if (CISM365Info.TryGet(check.GetType().Name, out CISM365Info info))
            {
                item.Version = info.Version;
                item.Section = info.Section;
                item.Level = info.Level.ToString();
                item.CISDocument = "CIS M365";
            }

            if (CISAzInfo.TryGet(check.GetType().Name, out CISAzInfo azInfo))
            {
                item.Version = azInfo.Version;
                item.Section = azInfo.Section;
                item.Level = azInfo.Level.ToString();
                item.CISDocument = "CIS Azure";
            }
            return item;
        }

        public static string createJSON(Auditor auditor)
        {
            List<ResultJSONItem> FindingList = new List<ResultJSONItem>();

            foreach (BaseCheck check in auditor.Finding)
            {
                ResultJSONItem item = GetResultJSONItem(check);
                if (check.RawData != null)
                {
                    item.RawData = check.RawData;
                }

                if (check.GetAffectedEntity().Count > 0)
                {
                    List<AffectedItem> affectedItems = new List<AffectedItem>();
                    foreach (IReporting entity in check.GetAffectedEntity())
                    {
                        affectedItems.Add(entity.GetAffectedItem());
                    }
                    item.AffectedItems = affectedItems;
                }
                FindingList.Add(item);
            }

            ResultJSONList FinalList = new ResultJSONList();
            FinalList.Finding = FindingList.OrderBy(x => x.RiskScore).ToList();
            FinalList.NoFinding = auditor.NoFinding
                .Select(x => GetResultJSONItem(x))
                .OrderBy(x => x.RiskScore).ToList();
            FinalList.Error = auditor.Error
                .Select(x => GetResultJSONItem(x))
                .OrderBy(x => x.RiskScore).ToList();
            FinalList.NotApplicable = auditor.NotApplicable
                .Select(x => GetResultJSONItem(x))
                .OrderBy(x => x.RiskScore).ToList();

            var options = new JsonSerializerOptions
            {
                MaxDepth = 16,
                IncludeFields = true,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(FinalList, options);
        }
    }
}
