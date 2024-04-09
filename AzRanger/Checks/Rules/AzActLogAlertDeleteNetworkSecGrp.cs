﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks.Rules
{
    internal class AzActLogAlertDeleteNetworkSecGrp : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            string operationCondition = "microsoft.network/networksecuritygroups/delete";
            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                List<string> scopes = new List<string>();
                foreach (ActivityLogAlert alert in sub.Resources.ActivityLogAlerts)
                {
                    if (alert.location == "Global" && alert.properties.enabled)
                    {
                        foreach (ActivityLogAlertAllof condition in alert.properties.condition.allOf)
                        {
                            if (condition.field == "operationName" && condition.equals.ToLower() == operationCondition)
                            {
                                foreach (String scope in alert.properties.scopes)
                                {
                                    if (!scopes.Contains(scope))
                                    {
                                        scopes.Add(scope);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (NetworkSecurityGroup resource in sub.Resources.NetworkSecurityGroups)
                {
                    bool isInscope = false;
                    foreach (String scope in scopes)
                    {
                        if (resource.id.Contains(scope))
                        {
                            isInscope = true;
                            break;
                        }
                    }
                    if (!isInscope)
                    {
                        this.AddAffectedEntity(resource);
                        passed = false;
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
