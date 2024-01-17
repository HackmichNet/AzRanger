using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
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
