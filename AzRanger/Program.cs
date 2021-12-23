using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Output;
using AzRanger.Utilities;
using CommandLine;
using System;
using System.Collections.Generic;
using AzRanger.AzScanner;
using NLog.Config;
using NLog.Targets;
using NLog;
using CommandLine.Text;

namespace AzRanger
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            PrintBanner();
            var parser = new CommandLine.Parser(with => with.HelpWriter = null);
            var parserResult = parser.ParseArguments<CommandlineOptions>(args);
            parserResult
              .WithParsed<CommandlineOptions>(options => RunOptions(options))
              .WithNotParsed(errs => DisplayHelp(parserResult, errs));
        }

        private static void DisplayHelp(ParserResult<CommandlineOptions> parserResult, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(parserResult, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "AzRanger 0.0.1"; //change header
                h.Copyright = ""; 
                return HelpText.DefaultParsingErrorsHandler(parserResult, h);
            }, e => e);
            Console.WriteLine(helpText);
        }


        private static void PrintBanner()
        {
            String banner = @"    _       ___                        
   /_\   __| _ \__ _ _ _  __ _ ___ _ _ 
  / _ \ |_ /   / _` | ' \/ _` / -_) '_|
 /_/ \_\/__|_|_\__,_|_||_\__, \___|_|  
                         |___/         ";
            Console.WriteLine();
            Console.WriteLine(banner);
            Console.WriteLine();
        }

        static void RunOptions(CommandlineOptions opts)
        {
            var config = new LoggingConfiguration();
            var consoleTargetDebug = new ConsoleTarget
            {
                Name = "console",
                Layout = "${level:uppercase=true}: ${message}",
            };
            if (opts.Debug)
            {
                if (opts.Logfile != null)
                {
                    var fileTarget = new FileTarget
                    {
                        Name = "file",
                        Layout = "${level:uppercase=true}: ${message}",
                        FileName = opts.Logfile,
                    };
                    config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget, "*");
                }
                else
                {
                    config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTargetDebug, "*");
                }
            }
            else
            {
                config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTargetDebug, "*");
            }
            LogManager.Configuration = config;
          
            if(opts.Audit && opts.DumpAll && opts.DumpSettings)
            {
                Console.WriteLine("[-] Please choose between --audit, --dumpsettings and --dumpall. Choose wisely.");
                return;
            }

            if (opts.Audit && opts.DumpAll)
            {
                Console.WriteLine("[-] Please choose between --audit, --dumpsettings and --dumpall. Choose wisely.");
                return;
            }

            if (opts.Audit && opts.DumpSettings)
            {
                Console.WriteLine("[-] Please choose between --audit, --dumpsettings and --dumpall. Choose wisely.");
                return;
            }

            if (opts.DumpAll && opts.DumpSettings)
            {
                Console.WriteLine("[-] Please choose between --audit, --dumpsettings and --dumpall. Choose wisely.");
                return;
            }

            if (opts.Audit == false && opts.DumpAll == false && opts.DumpSettings == false )
            {
                Console.WriteLine("[-] Please choose between --audit, --dumpsettings and --dumpall. Choose wisely.");
                return;
            }

            if(opts.DumpAll | opts.DumpSettings)
            {
                if(opts.OutFile == null)
                {
                    Console.WriteLine("[-] Please specify an outfile with --outfile");
                    return;
                }
            }

            Console.WriteLine("[+] AzRanger started.");
            Scanner scanner = null;
            if (opts.Username != null && opts.Password != null)
            {
                scanner = new Scanner(opts.Username, opts.Password, opts.Proxy);//, opts.Proxy);
            }
            else
            {
                scanner = new Scanner(opts.Proxy);
            }

            if (opts.DumpAll | opts.Audit)
            {
                Tenant tenant = scanner.ScanTenant();

                if (tenant == null)
                {
                    Console.WriteLine("[-] Something went wrong. Please run the tool with --debug and notify me.");
                    return;
                }

                if (opts.Audit)
                {
                    Auditor auditor = new Auditor(tenant);
                    auditor.Init(new Scope[]{
                        Scope.O365, Scope.EXO, Scope.SPO
                    });
                    auditor.PerformAudit();
                    ConsoleOutput.Print(auditor);
                }
                if (opts.DumpAll)
                {
                    Dumper.DumpTenant(tenant, opts.OutFile);
                    Console.WriteLine("[+] Successfully writen to " + opts.OutFile);
                }
                return;
            }

            if (opts.DumpSettings)
            {
                TenantSettings settings = scanner.ScanSettings();
                Dumper.DumpTenantSettings(settings, opts.OutFile);
                Console.WriteLine("[+] Successfully writen to " + opts.OutFile);
            }

            Console.WriteLine("[+] AzRanger finished... Press any key to exit!");
            Console.ReadKey();
        }
    }
}
