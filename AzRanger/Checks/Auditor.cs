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
        public List<BaseCheck> Failed;
        public List<BaseCheck> Passed;
        public List<BaseCheck> Error;

        private Tenant Tenant;

        public Auditor(Tenant tenant)
        {
            this.AllChecks = new List<BaseCheck>();
            this.Failed = new List<BaseCheck>();
            this.Passed = new List<BaseCheck>();
            this.Error = new List<BaseCheck>();
            this.Tenant = tenant;
        }

        public void Init(Scope[] scopes)
        {
            string RuleNamepace = "AzRanger.Checks.Rules";
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(t.IsClass && t.Namespace == RuleNamepace)
                {
                    try
                    {
                        BaseCheck check = GetInstance(t);
                        RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
                        foreach (Scope scope in scopes) {
                            if (ruleInfo.Scope.Equals(scope)){
                                logger.Debug("Auditor.Init: {0} successfull instatiated and added", ruleInfo.ShortName);
                                AllChecks.Add(check);
                            }
                        }
                    }
                    catch(Exception e) {
                        
                        logger.Debug("Auditor.Init: CreateClass failed");
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
                        case CheckResult.Failed:
                            this.Failed.Add(check);
                            break;
                        case CheckResult.Passed:
                            this.Passed.Add(check);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    RuleInfoAttribute ruleInfo = (RuleInfoAttribute)Attribute.GetCustomAttribute(check.GetType(), typeof(RuleInfoAttribute));
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
