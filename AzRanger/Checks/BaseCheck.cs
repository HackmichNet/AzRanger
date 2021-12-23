using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks
{
    public abstract class BaseCheck
    {

        private List<IEntity> AffectedEntity = new List<IEntity>();
        public abstract CheckResult Audit(Tenant tenant);

        public void AddAffectedEntity(IEntity entity)
        {
            if (!this.AffectedEntity.Contains(entity))
            {
                this.AffectedEntity.Add(entity);
            }
        }

        public List<IEntity> GetAffectedEntity()
        {
            return this.AffectedEntity;
        }
    }

    public enum CheckResult
    {
        Passed,
        Failed
    }
}
