using AzRanger.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AzRanger.Checks
{
    public class Auditor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public List<BaseCheck> AllChecks;
        public List<BaseCheck> Finding;
        public List<BaseCheck> NoFinding;
        public List<BaseCheck> Error;
        public List<BaseCheck> NotApplicable;

        private Tenant Tenant;

        public Auditor(Tenant tenant)
        {
            this.AllChecks = new List<BaseCheck>();
            this.Finding = new List<BaseCheck>();
            this.NoFinding = new List<BaseCheck>();
            this.Error = new List<BaseCheck>();
            this.NotApplicable = new List<BaseCheck>();
            this.Tenant = tenant;
        }

        public void Init(List<ScopeEnum> scopes)
        {
            string RuleNamepace = "AzRanger.Checks.Rules";
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(t.IsClass && t.Namespace == RuleNamepace && t.IsSubclassOf(typeof(BaseCheck)))
                {
                    try
                    {
                        BaseCheck check = GetInstance(t);
                        if (RuleInfo.TryGet(check.GetType().Name, out RuleInfo info))
                        {
                            if (scopes.Contains(info.Scope))
                            {
                                logger.Debug($"[+] Auditor.Init: {info.ShortName} successful instantiated and added.");
                                AllChecks.Add(check);
                            }
                        }
                        else
                        {
                            logger.Error($"[-] Auditor.Init: No RuleMeta defined for {check.GetType().Name}");
                            continue;

                        }
                    } catch (Exception e)
                    {
                        logger.Error($"[-] Auditor.Init: CreateClass failed for {t.GetType().Name}!");
                        logger.Error($"[-] {e.Message}");
                        continue;
                    }
                }
            }
        }

        public void PerformAudit()
        {
            foreach(BaseCheck check in AllChecks)
            {
                try
                {
                    CheckResult result = check.Audit(this.Tenant);
                    switch (result)
                    {
                        case CheckResult.Finding:
                            this.Finding.Add(check);
                            break;
                        case CheckResult.NoFinding:
                            this.NoFinding.Add(check);
                            break;
                        case CheckResult.NotApplicable:
                              this.NotApplicable.Add(check);
                            break;
                        case CheckResult.Error:
                            this.Error.Add(check);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    logger.Error($"Failed to check rule {check.GetType().Name}");
                    logger.Debug($"Auditor.PerformAudit: {e.Message}");
                    this.Error.Add(check);
                    continue;
                }
            }
        }

        private BaseCheck GetInstance(Type type)
        {
            return (BaseCheck)Activator.CreateInstance(type);
        }

    }
}
