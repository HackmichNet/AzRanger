using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Teams
{
    public class TeamsMeetingPolicy
    {
        public object Description { get; set; }
        public bool AllowChannelMeetingScheduling { get; set; }
        public bool AllowMeetNow { get; set; }
        public bool AllowPrivateMeetNow { get; set; }
        public string MeetingChatEnabledType { get; set; }
        public bool AllowExternalNonTrustedMeetingChat { get; set; }
        public bool CopyRestriction { get; set; }
        public string LiveCaptionsEnabledType { get; set; }
        public string DesignatedPresenterRoleMode { get; set; }
        public bool AllowIPAudio { get; set; }
        public bool AllowIPVideo { get; set; }
        public string AllowEngagementReport { get; set; }
        public string AllowTrackingInReport { get; set; }
        public string IPAudioMode { get; set; }
        public string IPVideoMode { get; set; }
        public bool AllowAnonymousUsersToDialOut { get; set; }
        public bool AllowAnonymousUsersToStartMeeting { get; set; }
        public bool AllowAnonymousUsersToJoinMeeting { get; set; }
        public object BlockedAnonymousJoinClientTypes { get; set; }
        public object AllowedStreamingMediaInput { get; set; }
        public string ExplicitRecordingConsent { get; set; }
        public bool AllowLocalRecording { get; set; }
        public string AutoRecording { get; set; }
        public string ParticipantNameChange { get; set; }
        public bool AllowPrivateMeetingScheduling { get; set; }
        public string AutoAdmittedUsers { get; set; }
        public bool AllowCloudRecording { get; set; }
        public bool AllowRecordingStorageOutsideRegion { get; set; }
        public string RecordingStorageMode { get; set; }
        public bool AllowOutlookAddIn { get; set; }
        public bool AllowPowerPointSharing { get; set; }
        public bool AllowParticipantGiveRequestControl { get; set; }
        public bool AllowExternalParticipantGiveRequestControl { get; set; }
        public bool AllowSharedNotes { get; set; }
        public bool AllowWhiteboard { get; set; }
        public bool AllowTranscription { get; set; }
        public bool AllowNetworkConfigurationSettingsLookup { get; set; }
        public int MediaBitRateKb { get; set; }
        public string ScreenSharingMode { get; set; }
        public string VideoFiltersMode { get; set; }
        public bool AllowPSTNUsersToBypassLobby { get; set; }
        public bool AllowOrganizersToOverrideLobbySettings { get; set; }
        public string PreferredMeetingProviderForIslandsMode { get; set; }
        public bool AllowNDIStreaming { get; set; }
        public string SpeakerAttributionMode { get; set; }
        public string EnrollUserOverride { get; set; }
        public string RoomAttributeUserOverride { get; set; }
        public string StreamingAttendeeMode { get; set; }
        public string ForceStreamingAttendeeMode { get; set; }
        public string AttendeeIdentityMasking { get; set; }
        public bool AllowBreakoutRooms { get; set; }
        public string TeamsCameraFarEndPTZMode { get; set; }
        public bool AllowMeetingReactions { get; set; }
        public bool AllowMeetingRegistration { get; set; }
        public string WhoCanRegister { get; set; }
        public string AllowScreenContentDigitization { get; set; }
        public bool AllowCarbonSummary { get; set; }
        public string RoomPeopleNameUserOverride { get; set; }
        public bool AllowMeetingCoach { get; set; }
        public int NewMeetingRecordingExpirationDays { get; set; }
        public string LiveStreamingMode { get; set; }
        public object MeetingInviteLanguages { get; set; }
        public string ChannelRecordingDownload { get; set; }
        public string AllowCartCaptionsScheduling { get; set; }
        public string AllowTasksFromTranscript { get; set; }
        public string InfoShownInReportMode { get; set; }
        public string LiveInterpretationEnabledType { get; set; }
        public string QnAEngagementMode { get; set; }
        public bool AllowImmersiveView { get; set; }
        public bool AllowAvatarsInGallery { get; set; }
        public bool AllowAnnotations { get; set; }
        public string AllowDocumentCollaboration { get; set; }
        public bool AllowWatermarkForScreenSharing { get; set; }
        public bool AllowWatermarkForCameraVideo { get; set; }
        public bool AllowWatermarkCustomizationForCameraVideo { get; set; }
        public int WatermarkForCameraVideoOpacity { get; set; }
        public string WatermarkForCameraVideoPattern { get; set; }
        public bool AllowWatermarkCustomizationForScreenSharing { get; set; }
        public int WatermarkForScreenSharingOpacity { get; set; }
        public string WatermarkForScreenSharingPattern { get; set; }
        public string WatermarkForAnonymousUsers { get; set; }
        public bool DetectSensitiveContentDuringScreenSharing { get; set; }
        public string AudibleRecordingNotification { get; set; }
        public string ConnectToMeetingControls { get; set; }
        public string Copilot { get; set; }
        public string AutomaticallyStartCopilot { get; set; }
        public string VoiceIsolation { get; set; }
        public string ExternalMeetingJoin { get; set; }
        public string ContentSharingInExternalMeetings { get; set; }
        public string AllowedUsersForMeetingContext { get; set; }
        public string SmsNotifications { get; set; }
        public string CaptchaVerificationForMeetingJoin { get; set; }
        public string UsersCanAdmitFromLobby { get; set; }
        public string LobbyChat { get; set; }
        public string AnonymousUserAuthenticationMethod { get; set; }
        public string NoiseSuppressionForDialInParticipants { get; set; }
        public string RealTimeText { get; set; }
        public string AIInterpreter { get; set; }
        public string VoiceSimulationInInterpreter { get; set; }
        public object DataSource { get; set; }
        public Key Key { get; set; }
        public string Identity { get; set; }
        public TeamsMeetingPolicyConfigmetadata ConfigMetadata { get; set; }
        public string ConfigId { get; set; }
    }

    public class TeamsMeetingPolicyKey
    {
        public string ScopeClass { get; set; }
        public TeamsMeetingPolicySchemaid SchemaId { get; set; }
        public TeamsMeetingPolicyAuthorityid AuthorityId { get; set; }
        public TeamsMeetingPolicyDefaultxml DefaultXml { get; set; }
        public TeamsMeetingPolicyXmlroot1 XmlRoot { get; set; }
    }

    public class TeamsMeetingPolicySchemaid
    {
        public TeamsMeetingPolicyXname XName { get; set; }
    }

    public class TeamsMeetingPolicyXname
    {
        public string name { get; set; }
    }

    public class TeamsMeetingPolicyAuthorityid
    {
        public string Class { get; set; }
        public string InstanceId { get; set; }
        public TeamsMeetingPolicyXmlroot XmlRoot { get; set; }
    }

    public class TeamsMeetingPolicyXmlroot
    {
        public string name { get; set; }
    }

    public class TeamsMeetingPolicyDefaultxml
    {
        public TeamsMeetingPolicySchemaid1 SchemaId { get; set; }
        public TeamsMeetingPolicyData Data { get; set; }
        public object ConfigObject { get; set; }
        public string Signature { get; set; }
        public bool IsModified { get; set; }
    }

    public class TeamsMeetingPolicySchemaid1
    {
        public TeamsMeetingPolicyXname1 XName { get; set; }
    }

    public class TeamsMeetingPolicyXname1
    {
        public string name { get; set; }
    }

    public class TeamsMeetingPolicyData
    {
        public TeamsMeetingPolicyTeamsmeetingpolicy TeamsMeetingPolicy { get; set; }
    }

    public class TeamsMeetingPolicyTeamsmeetingpolicy
    {
        public string xmlns { get; set; }
    }

    public class TeamsMeetingPolicyXmlroot1
    {
        public string name { get; set; }
    }

    public class TeamsMeetingPolicyConfigmetadata
    {
        public string Authority { get; set; }
    }

}
