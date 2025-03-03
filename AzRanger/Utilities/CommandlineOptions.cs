using AzRanger.Checks;
using CommandLine;
using System.Collections.Generic;


namespace AzRanger.Utilities
{
    class CommandlineOptions
    {
        [Option('u', "username", HelpText = "Specify the username.")]
        public string Username { get; set; }

        [Option('p', "password", HelpText = "Specifiy the password.")]
        public string Password { get; set; }

        [Option('c', "clientid", HelpText = "Specify the client id.")]
        public string ClientId { get; set; }

        [Option('s', "secret", HelpText = "Specify the client secret.")]
        public string ClientSecret { get; set; }

        [Option('t', "tenant", HelpText = "Specify a tenant.")]
        public string TenantId { get; set; }

        [Option(Required = false, HelpText = "Specify a proxy.")]
        public string Proxy { get; set; }

        [Option(Required = false, HelpText = "Enable verbose logging.")]
        public bool Debug { get; set; }

        [Option(Required = false, HelpText = "Set the logfile path.")]
        public string Logfile { get; set; }

        [Option(Required = false, HelpText = "Path/File to write results.")]
        public string OutPath { get; set; }

        [Option(Required = false, HelpText = "Write all results to console. Can result in a very large output.")]
        public bool WriteAllResults { get; set; }

        [Option(Required = false, HelpText = "Only for audit. Specify 'console', 'html' or 'json'.", Default = AzRangerOutput.HTML)]
        public AzRangerOutput Output { get; set; }

        [Option(Required = false, HelpText = "Set ScopeEnum AAD, Teams, SharePoint(SPO), ExchangeOnline(EXO), Azure or M365, which includes AAD, Teams, SPO and EXO. If not set all scopes will be used.", Separator = ',')]
        public IEnumerable<ScopeEnum> Scope { get; set; }
        [Option(Default = false, Required = false, HelpText = "Batch mode. Use for automatic runs.")]
        public bool Batch { get; set; }
        [Option(Required = false, HelpText = "AzRanger mode. Use audit, dumpall or dumpsettings. ", Default = AzRangerModes.Audit)]
        public AzRangerModes Mode { get; set; }
    }
}
