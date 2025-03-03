using AzRanger.AzScanner;
using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Output;
using AzRanger.Utilities;
using CommandLine;
using CommandLine.Text;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzRanger
{
    class Program
    {
        // String AzurePowerShell = "1950a258-227b-4e31-a9cf-717495945fc2";
        // Azure Active Directory Powershell = "1b730954-1685-4b74-9bfd-dac224a7b894"
        // AzureCli = "04b07795-8ddb-461a-bbee-02f9e1bf7b46"
        // MS Graph CommandLineTool = "14d82eec-204b-4c2f-b7e8-296a70dab67e"
        // PowerAutomate = "386ce8c0-7421-48c9-a1df-2a532400339f"
        // SPO Mgmt Shell = "9bc3ab49-b65d-410a-85ad-de819febfddc";

        private static string ClientID = "1b730954-1685-4b74-9bfd-dac224a7b894";
        private static string PowerAutomateID = "386ce8c0-7421-48c9-a1df-2a532400339f";
        private static string SPOMgmtShell = "9bc3ab49-b65d-410a-85ad-de819febfddc";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static async Task Main(string[] args)
        {
            PrintBanner();
            var parser = new CommandLine.Parser(settings =>
            {
                settings.HelpWriter = null;
                settings.CaseSensitive = false;
                settings.CaseInsensitiveEnumValues = true;
            });
            var parserResult = parser.ParseArguments<CommandlineOptions>(args);
            await parserResult.MapResult(
                    (CommandlineOptions opts) => RunOptions(opts),
                    errs => DisplayHelp(parserResult));
        }

        private static Task DisplayHelp(ParserResult<CommandlineOptions> parserResult)
        {
            var helpText = HelpText.AutoBuild(parserResult, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                h.Heading = "AzRanger 0.1.2"; //change header
                h.Copyright = "Made with love by @HackmichNet";
                return HelpText.DefaultParsingErrorsHandler(parserResult, h);
            }, e => e);
            Console.WriteLine(helpText);
            return Task.FromResult(1);
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

        private static async Task RunOptions(CommandlineOptions opts)
        {
            var config = new LoggingConfiguration();
            var consoleTargetDebug = new ConsoleTarget
            {
                Name = "console",
                Layout = "${level:uppercase=true}: ${message}",
            };
            if (opts.Debug)
            {
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTargetDebug, "*");
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
            }
            else
            {
                config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTargetDebug, "*");
            }
            LogManager.Configuration = config;

            String outputPath = null;
            if (opts.OutPath == null || opts.OutPath.Length == 0)
            {
                DateTime date = DateTime.Now;
                if (opts.Mode == AzRangerModes.Audit)
                {
                    outputPath = date.ToString("ddMMyyyy") + "_AZRangerReport";
                }
                if (opts.Mode == AzRangerModes.DumpAll | opts.Mode == AzRangerModes.DumpSettings)
                {
                    outputPath = date.ToString("ddMMyyyy") + "_AZRangerReport.json";

                }
            }
            else
            {
                outputPath = opts.OutPath;
            }

            List<ScopeEnum> scopes = new List<ScopeEnum>();
            if (opts.Scope != null)
            {
                foreach (ScopeEnum scope in opts.Scope)
                {
                    scopes.Add(scope);
                }
                if (scopes.Count == 0)
                {
                    scopes = new List<ScopeEnum>() {
                        ScopeEnum.Azure, ScopeEnum.SPO, ScopeEnum.EXO, ScopeEnum.Teams, ScopeEnum.AAD
                    };
                }
                else if (scopes.Count == 1 & scopes[0].Equals(ScopeEnum.M365))
                {
                    scopes = new List<ScopeEnum>() {
                        ScopeEnum.SPO, ScopeEnum.EXO, ScopeEnum.Teams, ScopeEnum.AAD
                    };
                }
            }

            Console.WriteLine("[+] AzRanger started.");
            MainCollector scanner = null;
            if (opts.Username != null && opts.Password != null)
            {
                String TenantId = opts.TenantId;
                if (TenantId == null)
                {
                    TenantId = Helper.GetTenantIdToDomain(opts.Username.Split('@')[1], opts.Proxy);
                }
                if (TenantId != null)
                {
                    UserAuthenticator aadPowerShellUserAuthenticator = new UserAuthenticator(opts.Username, opts.Password, TenantId, opts.Proxy, ClientID);
                    UserAuthenticator powerAutomateUserAuthenticator = new UserAuthenticator(opts.Username, opts.Password, TenantId, opts.Proxy, PowerAutomateID, "ms-appx-web://microsoft.aad.brokerplugin/386ce8c0-7421-48c9-a1df-2a532400339f");
                    UserAuthenticator spoMgmtShellAuthenticator = new UserAuthenticator(opts.Username, opts.Password, TenantId, opts.Proxy, SPOMgmtShell, "https://oauth.spops.microsoft.com/");
                    scanner = new MainCollector(aadPowerShellUserAuthenticator, powerAutomateUserAuthenticator, spoMgmtShellAuthenticator, opts.Proxy, TenantId);
                }
                else
                {
                    Console.WriteLine("[-] Could not find TenantId.... this should not happen, when providing the correct username.");
                    if (opts.Batch == false)
                    {
                        Helper.PressKeyToContinue("[+] AzRanger finished... Press any key to exit!");
                    }
                    return;
                }
            }
            else if (opts.ClientId != null && opts.ClientSecret != null)
            {
                if (opts.TenantId == null)
                {
                    Console.WriteLine("[-] You must provide the TenantId, when using application id and secret.");
                    if (opts.Batch == false)
                    {
                        Helper.PressKeyToContinue("[+] AzRanger finished... Press any key to exit!");
                    }
                    return;
                }
                AppAuthenticator authenticator = new AppAuthenticator(opts.ClientId, opts.ClientSecret, opts.TenantId, opts.Proxy);
                scanner = new MainCollector(authenticator, authenticator, authenticator, opts.Proxy, opts.TenantId);
            }
            else
            {
                UserAuthenticator aadPowerShellUserAuthenticator = new UserAuthenticator(opts.TenantId, opts.Proxy, ClientID);
                UserAuthenticator powerAutomateUserAuthenticator = new UserAuthenticator(opts.TenantId, opts.Proxy, PowerAutomateID, "ms-appx-web://microsoft.aad.brokerplugin/386ce8c0-7421-48c9-a1df-2a532400339f");
                UserAuthenticator spoMgmtShellAuthenticator = new UserAuthenticator(opts.TenantId, opts.Proxy, SPOMgmtShell, "https://oauth.spops.microsoft.com/");
                scanner = new MainCollector(aadPowerShellUserAuthenticator, powerAutomateUserAuthenticator, spoMgmtShellAuthenticator, opts.Proxy, opts.TenantId);
            }

            if (opts.Mode == AzRangerModes.Audit | opts.Mode == AzRangerModes.DumpAll)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Tenant tenant = await scanner.ScanTenant(scopes);
                watch.Stop();
                Console.WriteLine($"[+] Scan Time: {watch.ElapsedMilliseconds} ms");
                if (tenant == null)
                {
                    if (!opts.Debug)
                    {
                        Console.WriteLine("[-] Something went wrong. Please run the tool with --debug and notify me.");
                    }
                    if (opts.Batch == false)
                    {
                        Helper.PressKeyToContinue("[+] AzRanger finished... Press any key to exit!");
                    }
                    return;
                }

                if (opts.Mode == AzRangerModes.Audit)
                {
                    Auditor auditor = new Auditor(tenant);
                    auditor.Init(scopes);
                    auditor.PerformAudit();
                    if (opts.Output == AzRangerOutput.Console)
                    {
                        ConsoleOutput.Print(auditor, opts.WriteAllResults);
                    }
                    else if (opts.Output == AzRangerOutput.HTML)
                    {
                        HTMLReportingOutput.Print(auditor, tenant, outputPath);
                        Console.WriteLine("[+] Report written to: " + outputPath);
                    }
                    else if (opts.Output == AzRangerOutput.JSON)
                    {
                        JSONOutput.Print(auditor, outputPath);
                        Console.WriteLine("[+] Report written to: " + outputPath);
                    }
                    else
                    {
                        Console.WriteLine("[-] Should not happen =)");
                        if (opts.Batch == false)
                        {
                            Helper.PressKeyToContinue("[+] AzRanger finished... Press any key to exit!");
                        }
                        return;
                    }
                }
                if (opts.Mode == AzRangerModes.DumpAll)
                {
                    JSONDumper.WriteToFile(tenant, outputPath);
                    Console.WriteLine("[+] Successfully written to " + outputPath);
                }
            }

            if (opts.Mode == AzRangerModes.DumpSettings)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Tenant settings = await scanner.ScanTenant(scopes);
                watch.Stop();
                Console.WriteLine($"[+] Scan Time: {watch.ElapsedMilliseconds} ms");
                JSONDumper.WriteToFile(settings, outputPath);
                Console.WriteLine("[+] Successfully written to " + outputPath);
            }

            if (opts.Batch == false)
            {
                Helper.PressKeyToContinue("[+] AzRanger finished... Press any key to exit!");
            }
        }
    }
}

