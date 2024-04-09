namespace AzRanger.Models.MSGraph
{
    public class ScopedRoleMember
    {
        public string id { get; set; }
        public string roleId { get; set; }
        public string administrativeUnitId { get; set; }
        public ScopedRoleMemberRolememberinfo roleMemberInfo { get; set; }
    }

    public class ScopedRoleMemberRolememberinfo
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string userPrincipalName { get; set; }
    }

}
