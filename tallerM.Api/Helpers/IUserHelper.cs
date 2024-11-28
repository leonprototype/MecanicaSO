using Microsoft.AspNetCore.Identity;
using tallerM.Shared.DTO;
using tallerM.Shared.Entities;

namespace tallerM.Api.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginDTO login);
        Task LogoutAsync();
    }
}
