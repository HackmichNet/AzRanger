
namespace AzRanger.Models.MainIAM
{
    public class DirectoryProperties
    {
        public string objectId { get; set; }
        public string displayName { get; set; }
        public bool usersCanRegisterApps { get; set; }
        public bool isAnyAccessPanelPreviewFeaturesAvailable { get; set; }
        public bool showMyGroupsFeature { get; set; }
        public object myGroupsFeatureValue { get; set; }
        public object myGroupsGroupId { get; set; }
        public object myGroupsGroupName { get; set; }
        public bool showMyAppsFeature { get; set; }
        public object myAppsFeatureValue { get; set; }
        public object myAppsGroupId { get; set; }
        public object myAppsGroupName { get; set; }
        public bool showUserActivityReportsFeature { get; set; }
        public object userActivityReportsFeatureValue { get; set; }
        public object userActivityReportsGroupId { get; set; }
        public object userActivityReportsGroupName { get; set; }
        public bool showRegisteredAuthMethodFeature { get; set; }
        public object registeredAuthMethodFeatureValue { get; set; }
        public object registeredAuthMethodGroupId { get; set; }
        public object registeredAuthMethodGroupName { get; set; }
        public bool usersCanAddExternalUsers { get; set; }
        public bool limitedAccessCanAddExternalUsers { get; set; }
        public bool restrictDirectoryAccess { get; set; }
        public bool groupsInAccessPanelEnabled { get; set; }
        public bool selfServiceGroupManagementEnabled { get; set; }
        public bool securityGroupsEnabled { get; set; }
        public object usersCanManageSecurityGroups { get; set; }
        public bool office365GroupsEnabled { get; set; }
        public object usersCanManageOfficeGroups { get; set; }
        public bool allUsersGroupEnabled { get; set; }
        public object scopingGroupIdForManagingSecurityGroups { get; set; }
        public object scopingGroupIdForManagingOfficeGroups { get; set; }
        public object scopingGroupNameForManagingSecurityGroups { get; set; }
        public object scopingGroupNameForManagingOfficeGroups { get; set; }
        public object objectIdForAllUserGroup { get; set; }
        public bool allowInvitations { get; set; }
        public bool isB2CTenant { get; set; }
        //  Administration portal => Restrict access to Azure AD administration portal
        public bool restrictNonAdminUsers { get; set; }
        public int enableLinkedInAppFamily { get; set; } // 1 = Off no LinkedIn, 0 = On LinkedIn is allowed
        public object[] toEnableLinkedInUsers { get; set; }
        public object[] toDisableLinkedInUsers { get; set; }
        public object linkedInSelectedGroupObjectId { get; set; }
        public object linkedInSelectedGroupDisplayName { get; set; }
    }

}
