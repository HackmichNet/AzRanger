using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsAllowAnonymousUserStartMeetings : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TeamsSettings.TeamsMeetingPolicy.AllowAnonymousUsersToStartMeeting == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}