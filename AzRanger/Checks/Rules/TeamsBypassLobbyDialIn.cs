using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsBypassLobbyDialIn : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TeamsMeetingPolicy.AllowPSTNUsersToBypassLobby == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}