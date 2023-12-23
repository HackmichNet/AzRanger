using AzRanger.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                if(t.IsClass && t.Namespace == RuleNamepace)
                {
                    try
                    {
                        BaseCheck check = GetInstance(t);
                        RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                        foreach (ScopeEnum scope in scopes) {
                            if (ruleInfo.Scope.Equals(scope)){
                                logger.Debug("[+] Auditor.Init: {0} successful instantiated and added.", ruleInfo.ShortName);
                                AllChecks.Add(check);
                            }
                        }
                    }
                    catch(Exception e) {
                        
                        logger.Debug("[-] Auditor.Init: CreateClass failed!");
                        logger.Debug(e.Message);
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
                    RuleMetaAttribute ruleInfo = (RuleMetaAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleMetaAttribute));
                    logger.Warn("Failing to check: {0}", ruleInfo.ShortName);
                    this.Error.Add(check);
                    logger.Debug("Auditor.PerformAudit: " + e.Message);
                }
            }
        }

        private BaseCheck GetInstance(Type type)
        {
            return (BaseCheck)Activator.CreateInstance(type);
        }

    }
}
