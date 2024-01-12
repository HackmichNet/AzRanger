﻿using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("TeamsExternalSharingProvider", ScopeEnum.Teams, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/teams-settings")]
    [CISM365("3.7", "", Level.L2, "v2.0")]
    
    class TeamsExternalSharingProvider : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TeamsClientConfiguration.AllowBox == false &&
                tenant.TeamsSettings.TeamsClientConfiguration.AllowDropBox == false &&
                tenant.TeamsSettings.TeamsClientConfiguration.AllowEgnyte == false &&
                tenant.TeamsSettings.TeamsClientConfiguration.AllowGoogleDrive == false &&
                tenant.TeamsSettings.TeamsClientConfiguration.AllowShareFile == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
