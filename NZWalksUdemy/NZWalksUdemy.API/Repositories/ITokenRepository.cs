using Microsoft.AspNetCore.Identity;

namespace NZWalksUdemy.API.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser identityUser, List<String> Roles);
    }
}
