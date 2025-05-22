using Microsoft.AspNetCore.Identity;

namespace nzWalksApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<String> roles);
    }
}
