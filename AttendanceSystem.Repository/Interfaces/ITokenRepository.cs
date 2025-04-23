using Microsoft.AspNetCore.Identity;

namespace AttendanceSystem.Repository.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
