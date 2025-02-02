using Latihan.RCL.Entity;
using Microsoft.AspNetCore.Components.Authorization;

namespace Latihan.RCL.Auth
{
    public interface ICustomAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task LogInAsync(User user);
        void Logout();
    }
}