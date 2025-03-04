﻿using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsExternalCommunicationInbound : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumerInbound == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
