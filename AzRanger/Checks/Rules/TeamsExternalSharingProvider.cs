using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("TeamsExternalSharingProvider", Scope.O365, MaturityLevel.Mature, "https://admin.teams.microsoft.com/company-wide-settings/teams-settings")]
    [RuleScore("Teams allows to use Box, DropBox, GoogleDrive, CitrixFiles or Egnyte as cloud storage provider", "Allowing thirt-party storage provider increases the risk of data leakage", 3)]
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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
