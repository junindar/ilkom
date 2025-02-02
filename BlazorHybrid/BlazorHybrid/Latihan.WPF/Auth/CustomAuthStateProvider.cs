using System.Security.Claims;
using System.Threading.Tasks;
using Latihan.RCL.Auth;
using Latihan.RCL.Entity;
using Microsoft.AspNetCore.Components.Authorization;

namespace Latihan.WPF.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider, ICustomAuthStateProvider
    {
        private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(new AuthenticationState(currentUser));

        public Task LogInAsync(User user)
        {
            var loginTask = LogInAsyncCore();
            NotifyAuthenticationStateChanged(loginTask);

            return loginTask;

            async Task<AuthenticationState> LogInAsyncCore()
            {
                var loginuser = await LoginWithExternalProviderAsync(user);
                currentUser = loginuser;


                return new AuthenticationState(currentUser);
            }
        }

        private Task<ClaimsPrincipal> LoginWithExternalProviderAsync(User user)
        {


            Claim[] claims = {
                new(ClaimTypes.Name, user.Nama), new(ClaimTypes.Role, user.Role),

            };

            var identity = new ClaimsIdentity(claims, "Custom");
            var authenticatedUser = new ClaimsPrincipal(identity);
            return Task.FromResult(authenticatedUser);
        }

        public void Logout()
        {
            currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(currentUser)));
        }
    }
}
