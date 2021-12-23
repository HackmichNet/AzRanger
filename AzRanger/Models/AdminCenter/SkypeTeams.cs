
namespace AzRanger.Models.AdminCenter
{
    public class SkypeTeams
    {
        public bool IsSkypeTeamsLicensed { get; set; }
        public Tenantcategorysetting[] TenantCategorySettings { get; set; }
        public Bots Bots { get; set; }
        public Miscellaneous Miscellaneous { get; set; }
        public Email Email { get; set; }
        public Cloudstorage CloudStorage { get; set; }
        public Teamsownedapps TeamsOwnedApps { get; set; }
        public Tenantownedapps TenantOwnedApps { get; set; }
        public Migrationstates MigrationStates { get; set; }
    }

    public class Bots
    {
        public Isbotsenabled IsBotsEnabled { get; set; }
        public Issideloadedbotsenabled IsSideLoadedBotsEnabled { get; set; }
        public Botsetting[] BotSettings { get; set; }
        public Isexternalappsenabledbydefault IsExternalAppsEnabledByDefault { get; set; }
    }

    public class Isbotsenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Issideloadedbotsenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isexternalappsenabledbydefault
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Botsetting
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public Isenabled IsEnabled { get; set; }
    }

    public class Isenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Miscellaneous
    {
        public Isorganizationtabenabled IsOrganizationTabEnabled { get; set; }
        public Isskypebusinessinteropenabled IsSkypeBusinessInteropEnabled { get; set; }
        public Istbotproactivemessagingenabled IsTBotProactiveMessagingEnabled { get; set; }
    }

    public class Isorganizationtabenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isskypebusinessinteropenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Istbotproactivemessagingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Email
    {
        public Isemailintochannelsenabled IsEmailIntoChannelsEnabled { get; set; }
        public object[] RestrictedSenderList { get; set; }
    }

    public class Isemailintochannelsenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Cloudstorage
    {
        public Box Box { get; set; }
        public Dropbox Dropbox { get; set; }
        public Googledrive GoogleDrive { get; set; }
        public Sharefile ShareFile { get; set; }
    }

    public class Box
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Dropbox
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Googledrive
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Sharefile
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Teamsownedapps
    {
        public Teamsownedappsetting[] TeamsOwnedAppSettings { get; set; }
    }

    public class Teamsownedappsetting
    {
        public string Id { get; set; }
        public Isenabled1 IsEnabled { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
    }

    public class Isenabled1
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Tenantownedapps
    {
        public object[] TenantOwnedAppSettings { get; set; }
    }

    public class Migrationstates
    {
        public Enableappsmigration EnableAppsMigration { get; set; }
        public Enableclientsettingsmigration EnableClientSettingsMigration { get; set; }
        public Enablemeetupsmigration EnableMeetupsMigration { get; set; }
        public Enablemessagingmigration EnableMessagingMigration { get; set; }
    }

    public class Enableappsmigration
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enableclientsettingsmigration
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enablemeetupsmigration
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enablemessagingmigration
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Tenantcategorysetting
    {
        public string TenantSkuCategory { get; set; }
        public Isskypeteamsenabled IsSkypeTeamsEnabled { get; set; }
        public Meetups Meetups { get; set; }
        public Funcontrol FunControl { get; set; }
        public Messaging Messaging { get; set; }
    }

    public class Isskypeteamsenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Meetups
    {
        public Isvideoenabled IsVideoEnabled { get; set; }
        public Isscreensharingenabled IsScreenSharingEnabled { get; set; }
        public Ischannelmeetingenabled IsChannelMeetingEnabled { get; set; }
        public Isprivatemeetingenabled IsPrivateMeetingEnabled { get; set; }
        public Isadhocchannelmeetingenabled IsAdhocChannelMeetingEnabled { get; set; }
        public Isprivatecallingenabled IsPrivateCallingEnabled { get; set; }
    }

    public class Isvideoenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isscreensharingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Ischannelmeetingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isprivatemeetingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isadhocchannelmeetingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isprivatecallingenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Funcontrol
    {
        public Isstickersenabled IsStickersEnabled { get; set; }
        public Iscustommemesenabled IsCustomMemesEnabled { get; set; }
        public Isgiphyenabled IsGiphyEnabled { get; set; }
        public Giphyratingtype GiphyRatingType { get; set; }
    }

    public class Isstickersenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Iscustommemesenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Isgiphyenabled
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Giphyratingtype
    {
        public string Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Messaging
    {
        public Enableownersdeleteallmessages EnableOwnersDeleteAllMessages { get; set; }
        public Enableuserdeleteownmessages EnableUserDeleteOwnMessages { get; set; }
        public Enableusereditownmessages EnableUserEditOwnMessages { get; set; }
        public Enableuserprivatechat EnableUserPrivateChat { get; set; }
    }

    public class Enableownersdeleteallmessages
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enableuserdeleteownmessages
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enableusereditownmessages
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }

    public class Enableuserprivatechat
    {
        public bool Value { get; set; }
        public bool EnableEditing { get; set; }
    }


}
