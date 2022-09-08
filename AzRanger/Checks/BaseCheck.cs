using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks
{
    public abstract class BaseCheck
    {

        private List<IReporting> AffectedEntity = new List<IReporting>();
        private String Reason = null;
        public abstract CheckResult Audit(Tenant tenant);

        public void SetReason(String reason)
        {
            this.Reason = reason;
        }

        public string GetReason() { return this.Reason; }

        public void AddAffectedEntity(IReporting entity)
        {
            if (!this.AffectedEntity.Contains(entity))
            {
                this.AffectedEntity.Add(entity);
            }
        }

        public List<IReporting> GetAffectedEntity()
        {
            return this.AffectedEntity;
        }
    }

    public enum CheckResult
    {
        NoFinding,
        Finding,
        NotApplicable
    }
}
