using Microsoft.AspNetCore.Identity;

namespace NZWalk.API.Repositories
{
    public interface ITokenReositry
    {
        string CreatJwtToken(IdentityUser user, List<string> roles);
    }
}
