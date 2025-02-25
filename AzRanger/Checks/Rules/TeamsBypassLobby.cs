using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsBypassLobby : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TeamsMeetingPolicy.AutoAdmittedUsers.ToLower().Equals("everyoneincompanyexcludingguests") |
                tenant.TeamsSettings.TeamsMeetingPolicy.AutoAdmittedUsers.ToLower().Equals("organizeronly"))
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}