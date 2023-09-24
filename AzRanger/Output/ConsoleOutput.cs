using AzRanger.Checks;
using AzRanger.Checks.Rules;
using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Output
{
    public static class ConsoleOutput
    {
        public static void Print(Auditor auditor, bool WriteAllResults)
        {
            Console.WriteLine();
            Console.WriteLine("==========================================================");
            Console.WriteLine();
            Console.WriteLine("[+] Here is the Audit Result:");
            Console.WriteLine();
            Console.WriteLine("[+] The scan has identified the following issues:");
            Console.WriteLine();


            foreach (BaseCheck result in auditor.Finding)
            {
                
                RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(result.GetType(), typeof(RuleMetaAttribute));
                RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(result.GetType(), typeof(RuleInfoAttribute));

                if (ruleScore == null)
                {
                    continue;
                }
                               
                Console.WriteLine("     [-] {0} - {1} ", ruleScore.ShortDescription, ruleScore.Risk);
                if(ruleInfo.PortalUrl != null)
                {
                    Console.WriteLine("         You can lookup the setting here: {0}", ruleInfo.PortalUrl);
                }
                if(ruleScore.ReferenceLink != null)
                {
                    Console.WriteLine("         You can find more information here: {0}", ruleScore.ReferenceLink);
                }
                

                if(result.GetAffectedEntity().Count > 0)
                {
                    Console.WriteLine("          - The following objects are affected: ");

                    if (WriteAllResults)
                    {
                        foreach (IReporting entity in result.GetAffectedEntity())
                        {
       
                            Console.WriteLine("              - " + entity.PrintConsole());
                        }
                    }
                    else
                    {
                        int maxOutput = 9;
                        int counter = 0;
                        foreach (IReporting entity in result.GetAffectedEntity())
                        {
                            if (counter == maxOutput)
                            {
                                break;
                            }
                            Console.WriteLine("              - " + entity.PrintConsole());
                            counter++;
                        }
                        if (result.GetAffectedEntity().Count > 10)
                        {
                            Console.WriteLine("          - For more output set parameter --WriteAllResults ");
                        }
                    }
                }
                Console.WriteLine();

            }
            
            List<BaseCheck> tentantiveChecks = new List<BaseCheck>();
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
                if (ruleInfo.MaturityLevel == MaturityLevel.Tentative)
                {
                    tentantiveChecks.Add(check);
                }
            }
            if(tentantiveChecks.Count > 0)
            {
                Console.WriteLine("[+] You may haved passed the following checks. Anyway I recommend to look it up in the portal:");
                Console.WriteLine();
                foreach (BaseCheck check in tentantiveChecks)
                {

                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                    
                    Console.WriteLine("     [-] {0}", ruleScore.ShortDescription);
                    if (ruleInfo.PortalUrl != null)
                    {
                        Console.WriteLine("         You can lookup the setting here: {0}", ruleInfo.PortalUrl);
                    }
                }

            }

            if (auditor.NotApplicable.Count > 0)
            {
                foreach (BaseCheck check in auditor.NotApplicable)
                {
                    Console.WriteLine("[+] The following checks were not applicable to your tenant:");
                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    RuleInfoAttribute ruleScore = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));

                    Console.WriteLine("     [+] {0}", ruleScore.ShortDescription);
                    if(check.GetReason() != null && check.GetReason().Length > 0)
                    {
                        Console.WriteLine("         Reason: {0}", check.GetReason());
                    }
                    if (ruleInfo.PortalUrl != null)
                    {
                        Console.WriteLine("         You can lookup the setting here: {0}", ruleInfo.PortalUrl);
                    }
                }
            }
            Console.WriteLine();
            if (auditor.Error.Count > 0)
            {
                Console.WriteLine("[+] The following checks failed for unknown reason - maybe you have not enough permissions:");
                foreach(BaseCheck error in auditor.Error)
                {
                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(error.GetType(), typeof(RuleMetaAttribute));
                    Console.WriteLine("          - {0}", ruleInfo.ShortName);
                    if (ruleInfo.PortalUrl != null)
                    {
                        Console.WriteLine("         You can lookup the setting manually here: {0}", ruleInfo.PortalUrl);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("[+] I would be happy if you let the tool run with: --debug and --logfile and give me feedback on the error");
                Console.WriteLine();
            }

            int numPerformedChecks = auditor.AllChecks.Count - auditor.Error.Count;
            Console.WriteLine();
            Console.WriteLine("[+] You have passed {0} from {1} checks.", auditor.NoFinding.Count, numPerformedChecks);
            Console.WriteLine();

        }

    }
}
