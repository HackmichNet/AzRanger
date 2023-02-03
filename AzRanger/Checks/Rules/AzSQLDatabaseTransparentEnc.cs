﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSQLDatabaseTransparentEnc", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", ServiceEnum.SQLServer)]
    [CISAZ("4.1.5", "", Level.L1, "v1.5")]
    [RuleInfo("SQL Database is not transparent encrypted", "This could be an addtional risk to the SQL Databases, as it simplifies attacks on the infrastructure.", 1, null, null, @"For each SQL Database except the database ""Master"" check under ""Transparent data encryption"" switch ""Data encryption"" to ""On"".")]
    internal class AzSQLDatabaseTransparentEnc : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.Resources.SQLServers == null)
                {
                    this.SetReason("You do not have SQLServers or the user cannot access them.");
                    return CheckResult.NotApplicable;
                }
                foreach (SQLServer server in sub.Resources.SQLServers)
                {
                    if(server.SQLDatabases != null)
                    {
                        foreach(SQLDatabase database in server.SQLDatabases)
                        {
                            // MasterDB cannot be encypted
                            if(database.name != "master")
                            {
                                if(database.transparentDataEncryption.properties.state != "Enabled")
                                {
                                    passed = false;
                                    this.AddAffectedEntity(database);
                                }
                            }
                        }
                    }
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}