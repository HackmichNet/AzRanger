﻿using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{    
    class UserGuestWithHighPrivRole : BaseCheck
    {
        bool passed = true;
        public override CheckResult Audit(Tenant tenant)
        {
            List<DirectoryRole> globalAdminRoles = new List<DirectoryRole>();
            foreach (String highPrivRole in DirectoryRoleTemplateID.GlobalAdmins)
            {
                foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
                {
                    if (role.roleTemplateId == highPrivRole)
                    {
                        globalAdminRoles.Add(role);
                        break;
                    }
                }
            }

            foreach(DirectoryRole role in globalAdminRoles)
            {
                foreach(AzurePrincipal principal in role.GetMembers())
                {
                    if (principal.PrincipalType == AzurePrincipalType.User)
                    {
                        if(tenant.AllGuests.ContainsKey(principal.id))
                        {
                            this.AddAffectedEntity(tenant.AllGuests[principal.id]);
                            passed = false;
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
