using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyEat.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, IList<string> Roles);
    }
}