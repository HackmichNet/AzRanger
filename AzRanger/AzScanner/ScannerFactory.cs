using NLog;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public static class ScannerFactory
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        private static async Task<IScannerModule> CreateScanner(IScannerModule scanner)
        {
            String accessToken = await scanner.Scanner.Authenticator.GetAccessToken(scanner.Scope);
            if (accessToken == null)
            {
                logger.Warn("IScanner.Get: {0}|{1} failed to get token!", scanner.GetType().ToString(), scanner.Scope.ToString());
                return null;
            }
            scanner.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return scanner;
        }

        internal static async Task<AdminCenterScanner> CreateAdminCenterScanner(Scanner scanner)
        {
            AdminCenterScanner scannerModul = new AdminCenterScanner(scanner);
            return (AdminCenterScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<AzMgmtScanner> CreateAzMgmtScanner(Scanner scanner)
        {
            AzMgmtScanner scannerModul = new AzMgmtScanner(scanner);
            return (AzMgmtScanner)await CreateScanner(scannerModul);
        }
        
        internal static async Task<AzrbacScanner> CreateAzrbacScanner(Scanner scanner)
        {
            AzrbacScanner scannerModul = new AzrbacScanner(scanner);
            return (AzrbacScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<ComplianceCenterScanner> CreateComplianceCenterScanner(Scanner scanner)
        {
            ComplianceCenterScanner scannerModul = new ComplianceCenterScanner(scanner);
            scannerModul = (ComplianceCenterScanner)await CreateScanner(scannerModul);
            scannerModul.BaseAdresse = await scannerModul.GetBaseAddress();
            return scannerModul;
        }

        internal static async Task<ExchangeOnlineScanner> CreateExchangeOnlineScanner(Scanner scanner)
        {
            ExchangeOnlineScanner scannerModul = new ExchangeOnlineScanner(scanner);
            return (ExchangeOnlineScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<GraphWinScanner> CreateGraphWinScanner(Scanner scanner)
        {
            GraphWinScanner scannerModul = new GraphWinScanner(scanner);
            return (GraphWinScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<KeyVaultScanner> CreateKeyVaultScanner(Scanner scanner, String vaultUri)
        {
            KeyVaultScanner scannerModul = new KeyVaultScanner(scanner, vaultUri);
            return (KeyVaultScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<MainIamScanner> CreateMainIamScanner(Scanner scanner)
        {
            MainIamScanner scannerModul = new MainIamScanner(scanner);
            return (MainIamScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<MSGraphScanner> CreateMSGraphScanner(Scanner scanner)
        {
            MSGraphScanner scannerModul = new MSGraphScanner(scanner);
            return (MSGraphScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<ProvisionAPIScanner> CreateProvisionAPIScanner(Scanner scanner)
        {
            ProvisionAPIScanner scannerModul = new ProvisionAPIScanner(scanner);
            return (ProvisionAPIScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<SharePointScanner> CreateSharePointScanner(Scanner scanner, string BaseAdresse)
        {
            SharePointScanner scannerModul = new SharePointScanner(scanner, BaseAdresse);
            return (SharePointScanner)await CreateScanner(scannerModul);
        }

        internal static async Task<TeamsScanner> CreateTeamsScanner(Scanner scanner)
        {
            TeamsScanner scannerModul = new TeamsScanner(scanner);
            return (TeamsScanner)await CreateScanner(scannerModul);
        }
    }
}
