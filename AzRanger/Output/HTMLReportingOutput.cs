using AzRanger.Checks;
using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    internal class HTMLReportingOutput
    {
        internal static string CardTemplate = @"<div class=""card"">
                                <div class=""card-header"">
                                    <h5 class=""mb-0"">
                                    <button class=""btn btn-link collapsed"" type=""button"" data-bs-toggle=""collapse"" data-bs-target=""#collapseCard{{CardID}}"">
                                        [{{Result}}] - {{Title}}
                                    </button>
                                    </h5>
                                </div>
                                <div id=""collapseCard{{CardID}}"" class=""collapse"">
                                    <div class=""card-body"">
                                       {{Description}}
                                    </div>
                                </div>
                            </div>";

        internal static String ErrorAccordion = @"<div class=""card"">
                        <div class=""card-header"">
                            <i class=""me-1""></i>
                            Error
                        </div>
                        <div class=""card-body change-content"">
                            <div class=""accordion"">
                                {{Errors}}
                            </div>
                        </div>
                    </div> ";


        public static void Print(Auditor auditor, Tenant tenant, String outFile)
        {
            String template = Properties.Resource.Report_html;
            int cardCounter = 0;

            template = template.Replace("{{TenantID}}", tenant.TenantId);
            template = template.Replace("{{Username}}", tenant.Username);
            StringBuilder m365NoFindings = new StringBuilder();
            StringBuilder azNoFindings = new StringBuilder();

            StringBuilder m365Findings = new StringBuilder();
            StringBuilder azFindings = new StringBuilder();

            StringBuilder m365Error = new StringBuilder();
            StringBuilder azError = new StringBuilder();

            int critical = 0;
            int high = 0;
            int medium = 0;
            int low = 0;
            int none = 0;
            string criticality = "";

            int numOfFindingsInReoprt = 0;
            int numOfOkInReport = 0;
            int numOfErrorInReport = 0;

            List<BaseCheck> checksToRemove = new List<BaseCheck>();

            foreach (BaseCheck check in auditor.Finding)
            {
                RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));

                if (ruleScore == null)
                {
                    continue;
                }
                RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                if (ruleInfo == null)
                {
                    continue;
                }
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                if(ruleScore.RiskScore == 0)
                {
                    none++;
                    criticality = "None";
                }
                if (ruleScore.RiskScore >= 1 && ruleScore.RiskScore <= 3)
                {
                    low++;
                    criticality = "Low";
                }
                if (ruleScore.RiskScore >= 4 && ruleScore.RiskScore <= 6)
                {
                    medium++;
                    criticality = "Medium";
                }
                if (ruleScore.RiskScore >= 7 && ruleScore.RiskScore <= 9)
                {
                    high++;
                    criticality = "High";
                }
                if (ruleScore.RiskScore == 10)
                {
                    critical++;
                    criticality = "Critical";
                }

                string html = String.Copy(CardTemplate);
                html = html.Replace("{{Result}}", criticality);
                html = html.Replace("{{Title}}", ruleScore.ShortDescription);
                html = html.Replace("{{Description}}", CreateDescription(ruleScore, ruleInfo, cisM365Rule, cisAzRule, ruleInfo.MaturityLevel, check.GetAffectedEntity()));
                html = html.Replace("{{CardID}}", cardCounter.ToString());
                cardCounter++;
                numOfFindingsInReoprt++;
                if (ruleInfo.Scope == ScopeEnum.AAD | ruleInfo.Scope == ScopeEnum.EXO | ruleInfo.Scope == ScopeEnum.SPO | ruleInfo.Scope == ScopeEnum.Teams)
                {
                    m365Findings.Append(html);
                }
                else
                {
                    azFindings.Append(html);
                }
            }

            foreach (BaseCheck check in auditor.NoFinding)
            {
                RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));

                if (ruleScore == null)
                {
                    continue;
                }
                RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                if (ruleInfo == null)
                {
                    continue;
                }
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                string html = String.Copy(CardTemplate);
                if (ruleInfo.MaturityLevel == MaturityLevel.Tentative)
                {
                    html = html.Replace("{{Result}}", "No Finding - Tentative");
                }
                else
                {
                    html = html.Replace("{{Result}}", "No Finding");
                }
                html = html.Replace("{{Title}}", ruleScore.ShortDescription);
                html = html.Replace("{{Description}}", CreateDescription(ruleScore, ruleInfo, cisM365Rule, cisAzRule, ruleInfo.MaturityLevel));
                html = html.Replace("{{CardID}}", cardCounter.ToString());
                cardCounter++;
                numOfOkInReport++;
                if (ruleInfo.Scope == ScopeEnum.AAD | ruleInfo.Scope == ScopeEnum.EXO | ruleInfo.Scope == ScopeEnum.SPO | ruleInfo.Scope == ScopeEnum.Teams)
                {
                    m365NoFindings.Append(html);
                }
                else
                {
                    azNoFindings.Append(html);
                }
            }

            foreach (BaseCheck check in auditor.Error)
            {
                RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));

                if (ruleScore == null)
                {
                    continue;
                }
                RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                if (ruleInfo == null)
                {
                    continue;
                }
                CISM365Attribute cisM365Rule = (CISM365Attribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISM365Attribute));
                CISAZAttribute cisAzRule = (CISAZAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(CISAZAttribute));

                string html = String.Copy(CardTemplate);
                html = html.Replace("{{Result}}", "Error");
                html = html.Replace("{{Title}}", ruleScore.ShortDescription);
                html = html.Replace("{{Description}}", CreateDescription(ruleScore, ruleInfo, cisM365Rule, cisAzRule, ruleInfo.MaturityLevel));
                html = html.Replace("{{CardID}}", cardCounter.ToString());
                cardCounter++;
                numOfErrorInReport++;
                if (ruleInfo.Scope == ScopeEnum.AAD | ruleInfo.Scope == ScopeEnum.EXO | ruleInfo.Scope == ScopeEnum.SPO)
                {
                    m365Error.Append(html);
                }
                else
                {
                    azError.Append(html);
                }
            }

            template = template.Replace("{{M365OK}}", m365NoFindings.ToString());
            template = template.Replace("{{AzOk}}", azNoFindings.ToString());
            template = template.Replace("{{M365Findings}}", m365Findings.ToString());
            template = template.Replace("{{AZFindings}}", azFindings.ToString());
            template = template.Replace("{{Critical}}", critical.ToString());
            template = template.Replace("{{High}}", high.ToString());
            template = template.Replace("{{Medium}}", medium.ToString());
            template = template.Replace("{{Low}}", low.ToString());
            template = template.Replace("{{None}}", none.ToString());

            if (m365Error.Length > 0)
            {
                template = template.Replace("{{M365Error}}", ErrorAccordion);
                template = template.Replace("{{Errors}}", m365Error.ToString());
            }
            else
            {
                template = template.Replace("{{M365Error}}", "");
            }
            if(azError.Length >0)
            {
                template = template.Replace("{{AzError}}", ErrorAccordion);
                template = template.Replace("{{Errors}}", azError.ToString());
            }
            else
            {
                template = template.Replace("{{AzError}}", "");
            }


            // Ratings for PieChart
            template = template.Replace("{{NumNoFindings}}", numOfOkInReport.ToString());
            template = template.Replace("{{NumFindings}}", numOfFindingsInReoprt.ToString());
            template = template.Replace("{{NumError}}", numOfErrorInReport.ToString());

            File.WriteAllText(outFile, template);

        }

        internal static String CreateDescription(RuleInfoAttribute ruleInfo, RuleMetaAttribute ruleMetaInfo, CISM365Attribute cisM365Rule, CISAZAttribute cisAzRule, MaturityLevel maturity, List<IReporting> affectedEntities = null )
        {
            String result = "";
            if (maturity == MaturityLevel.Tentative)
            {
                result = result + "<b>It is not possible to carry out the test completely automatically. Please check again manually.</b>  </br> </br>";
            }
            result = result + "<b>Risk:</b> </br> " + ruleInfo.Risk;

            if(ruleInfo.LongDescription != null)
            {
                result = result + "</br> </br> <b>Details:</b>  </br>" + ruleInfo.LongDescription;
            }
            if(affectedEntities != null && affectedEntities.Count > 0)
            {
                result = result + "</br> </br> <b>Affected Entities:</b>  </br>";
                result = result + @"<ul class=""list - group"">";
                foreach (IReporting entity in affectedEntities)
                {
                    result = result + @"<li class=""list-group-item"">" + entity.PrintConsole() + "</li>";
                }
                result = result + @"</ul>";
            }
            if(ruleInfo.Solution != null)
            {
                result = result + "</br></br> <b>Solution:</b> </br> " + ruleInfo.Solution;
            }
            if (ruleMetaInfo.PortalUrl != null)
            {
                result = result + "</br> </br> <b>Portal Url:</b> </br> " + ruleMetaInfo.PortalUrl;
            }
            if (ruleInfo.RefernceLink != null | cisM365Rule != null | cisAzRule != null)
            {
                result = result + "</br></br> <b>Reference:</b>";
                if (ruleInfo.RefernceLink != null)
                {
                    result = result + @"</br> More information under: </br> <a href=""" + ruleInfo.RefernceLink + @""">" + ruleInfo.RefernceLink + "</a>" ;
                }

                if (cisM365Rule != null)
                {
                    result = result + "</br>CIS Microsoft 365 Foundations Benchmark " + cisM365Rule.Version + ": " + cisM365Rule.Section;
                }
                if (cisAzRule != null)
                {
                    result = result + "</br>CIS Microsoft Azure Foundations Benchmark " + cisAzRule.Version + ": " + cisAzRule.Section;
                }
            }
            if (ruleMetaInfo.MaturityLevel == MaturityLevel.Tentative)
            {
                result = result + "</br></br> <b>Note:</b> This rule may be a false positive";
            }
            return result;
        }
    }
}
