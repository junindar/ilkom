using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;


namespace Latihan4.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(new AuthenticationState(currentUser));

        public Task LogInAsync()
        {
            var loginTask = LogInAsyncCore();
            NotifyAuthenticationStateChanged(loginTask);

            return loginTask;

            async Task<AuthenticationState> LogInAsyncCore()
            {
                var user = await LoginWithExternalProviderAsync();
                currentUser = user;

                return new AuthenticationState(currentUser);
            }
        }

        private Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
        {

            Claim[] claims = {
                new(ClaimTypes.Name, "Junindar"), new(ClaimTypes.Role, "Admin"),

            };

            var identity = new ClaimsIdentity(claims,"Custom");
           
            var authenticatedUser = new ClaimsPrincipal(identity);
            return Task.FromResult(authenticatedUser);
        }

        private Task<ClaimsPrincipal> LoginWithExternalProviderAsync2()
        {
            /*
                Provide OpenID/MSAL code to authenticate the user. See your identity 
                provider's documentation for details.

                Return a new ClaimsPrincipal based on a new ClaimsIdentity.
            */
            // var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
            //var identity = new ClaimsIdentity("Custom");
            //Claim[] claims = [
            //    new Claim(ClaimTypes.Name, "Junindar")
            //    ];

            Claim[] claims = {
                new(ClaimTypes.Name, "Junindar"), new(ClaimTypes.Role, "Admin"),

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
