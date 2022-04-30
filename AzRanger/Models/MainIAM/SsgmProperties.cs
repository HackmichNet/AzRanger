using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MainIAM
{
    public class SsgmProperties
    {
        public string objectId { get; set; }
        public object displayName { get; set; }
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
        public string usersCanManageSecurityGroups { get; set; }
        public bool office365GroupsEnabled { get; set; }
        public string usersCanManageOfficeGroups { get; set; }
        public bool allUsersGroupEnabled { get; set; }
        public string scopingGroupIdForManagingSecurityGroups { get; set; }
        public string scopingGroupIdForManagingOfficeGroups { get; set; }
        public string scopingGroupNameForManagingSecurityGroups { get; set; }
        public string scopingGroupNameForManagingOfficeGroups { get; set; }
        public string objectIdForAllUserGroup { get; set; }
        public bool allowInvitations { get; set; }
        public bool isB2CTenant { get; set; }
        public bool restrictNonAdminUsers { get; set; }
        public int enableLinkedInAppFamily { get; set; }
        public object toEnableLinkedInUsers { get; set; }
        public object toDisableLinkedInUsers { get; set; }
        public object linkedInSelectedGroupObjectId { get; set; }
        public object linkedInSelectedGroupDisplayName { get; set; }
    }

}
