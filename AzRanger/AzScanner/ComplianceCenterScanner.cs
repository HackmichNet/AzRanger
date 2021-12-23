using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class ComplianceCenterScanner : IScanner
    {
        public const String DLPPolicies = "/Psws/service.svc/DlpCompliancePolicy";
        public ComplianceCenterScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://deu01b.restapi.compliance.protection.outlook.com";
            //this.BaseAdresse = "https://ps.compliance.protection.outlook.com";
            this.ClientID = "1b730954-1685-4b74-9bfd-dac224a7b894";
            this.Scope = new String[] { "https://ps.compliance.protection.outlook.com/.default", "offline_access", "openid", "profile"};
        }

        public List<DlpCompliancePolicy> GetDLPPolicies()
        {
            return GetAllOf<DlpCompliancePolicy>(DLPPolicies, null, null);

        }
    }
}
