using AzRanger.Checks;
using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Utilities
{
    class CommandlineOptions
    {
        [Option('u', "username", HelpText = "Specify the username.")]
        public String Username{ get; set; }

        [Option('p', "password", HelpText = "Specifiy the password.")]
        public String Password { get; set; }

        [Option('t', "tenant", HelpText = "Specifiy a tenand.")]
        public String TenantId { get; set; }

        [Option(Required = false, HelpText = "Specifiy a proxy.")]
        public String Proxy { get; set; }

        [Option(Required = false, HelpText = "Enable verbose logging.")]
        public bool Debug { get; set; }

        [Option(Required = false, HelpText = "Set the logfile path.")]
        public String Logfile{ get; set; }

        [Option(Required = false, HelpText = "Perform an audit against your tenant.")]
        public bool Audit { get; set; }

        [Option(Required = false, HelpText = "Dump all information the tools gather into JSON.")]
        public bool DumpAll { get; set; }

        [Option(Required = false, HelpText = "Dump all tenant settings the tool gathers into JSON.")]
        public bool DumpSettings { get; set; }

        [Option(Required = false, HelpText = "File to write results.")]
        public String OutFile { get; set; }

        [Option(Required = false, HelpText = "Write all results to console. Can result in a very large output.")]
        public bool WriteAllResults { get; set; }

        [Option(Required = false, HelpText = "Only for audit. Specify 'console' (standard) or 'html'.")]
        public string Output { get; set; }
        [Option(Required = false, HelpText = "Set ScopeEnum AAD, Teams, SharePoint(SPO), ExchangeOnline(EXO) or Azure. If not set all scopes will be used.", Separator = ',')]
        public IEnumerable<ScopeEnum> Scope { get; set; }
    }
}
