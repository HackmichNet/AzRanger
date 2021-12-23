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
        [Option('u', "username", HelpText = "Specify the Username.")]
        public String Username{ get; set; }

        [Option('p', "password", HelpText = "Specifiy the password.")]
        public String Password { get; set; }

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

        [Option(Required = false, HelpText = "Dump all the tenant settings the tool gathers into JSON.")]
        public bool DumpSettings { get; set; }

        [Option(Required = false, HelpText = "File to write.")]
        public String OutFile { get; set; }

    }
}
