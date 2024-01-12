using AzRanger.Checks;
using AzRanger.Models;
using System;
using System.Collections.Generic;

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

                if (!RuleInfo.TryGet(result.GetType().Name, out RuleInfo info)) {
                    continue;
                }
                
                Console.WriteLine("     [-] {0} - {1} ", info.ShortDescription, info.Risk);
                if(ruleInfo.PortalUrl != null)
                {
                    Console.WriteLine("         You can lookup the setting here: {0}", ruleInfo.PortalUrl);
                }
                if(info.ReferenceLink != null)
                {
                    Console.WriteLine("         You can find more information here: {0}", info.ReferenceLink);
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
                    if (!RuleInfo.TryGet(check.GetType().Name, out RuleInfo info))
                    {
                        continue;
                    }

                    Console.WriteLine("     [-] {0}", info.ShortDescription);
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
                    if (!RuleInfo.TryGet(check.GetType().Name, out RuleInfo info))
                    {
                        continue;
                    }

                    Console.WriteLine("     [+] {0}", info.ShortDescription);
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
