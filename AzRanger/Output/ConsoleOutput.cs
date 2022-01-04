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
        public static void Print(Auditor auditor)
        {
            Console.WriteLine();
            Console.WriteLine("==========================================================");
            Console.WriteLine();
            Console.WriteLine("[+] Here is the Audit Result:");
            Console.WriteLine();
            Console.WriteLine("[+] The scan has identified the following issues:");
            Console.WriteLine();


            foreach (BaseCheck result in auditor.Failed)
            {
                
                RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(result.GetType(), typeof(RuleInfoAttribute));
                RuleScoreAttribute ruleScore = (RuleScoreAttribute)Attribute.GetCustomAttribute(result.GetType(), typeof(RuleScoreAttribute));

                
                
                Console.WriteLine("     [-] {0} - {1} ", ruleScore.Description, ruleScore.Rational);
                if(ruleInfo.PortalUrl != null)
                {
                    Console.WriteLine("         You can lookup the setting here: {0}", ruleInfo.PortalUrl);
                }
                if(ruleScore.Link != null)
                {
                    Console.WriteLine("         You can find more information here: {0}", ruleScore.Link);
                }
                

                if(result.GetAffectedEntity().Count > 0)
                {
                    Console.WriteLine("          - The following objects are affected: ");
                    int maxOutput = 9;
                    int counter = 0;
                    foreach (IEntity entity in result.GetAffectedEntity())
                    {
                        if(counter == maxOutput)
                        {
                            break;
                        }
                        Console.WriteLine("              - " + entity.PrintConsole());
                        counter++;
                    }
                    if(result.GetAffectedEntity().Count > 10)
                    {
                        Console.WriteLine("          - For more output set parameter <TBD> ");
                    }
                }
                Console.WriteLine();

            }
            
            List<BaseCheck> tentantiveChecks = new List<BaseCheck>();
            foreach (BaseCheck check in auditor.Passed)
            {             
                RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));  
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

                    RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                    RuleScoreAttribute ruleScore = (RuleScoreAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleScoreAttribute));
                    
                    Console.WriteLine("     [-] {0}", ruleScore.Description);
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
                    RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(error.GetType(), typeof(RuleInfoAttribute));
                    Console.WriteLine("          - {0}", ruleInfo.ShortName);
                }

                Console.WriteLine();
                Console.WriteLine("[+] I would be happy if you let the tool run with: --debug and --logfile and give me feedback on the error");
                Console.WriteLine();
            }

            int numPerformedChecks = auditor.AllChecks.Count - auditor.Error.Count;
            Console.WriteLine();
            Console.WriteLine("[+] You have passed {0} from {1} checks.", auditor.Passed.Count, numPerformedChecks);
            Console.WriteLine();

        }

    }
}
