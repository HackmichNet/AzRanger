using System;
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
