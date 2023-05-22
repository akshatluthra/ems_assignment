using Microsoft.AspNetCore.Identity;

namespace Employee_management_System_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
