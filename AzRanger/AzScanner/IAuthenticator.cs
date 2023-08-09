using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public interface IAuthenticator
    {
        Task<String> GetUserId();
        Task<String> GetAccessToken(String[] scopes);
        Task<String> GetTenantId();
        Task<String> GetUsername();
    }
}
