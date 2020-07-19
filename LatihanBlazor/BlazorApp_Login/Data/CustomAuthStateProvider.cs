using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorAPP_Login.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorAPP_Login.Data
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var identity = new ClaimsIdentity();


            var claimsPrincipal = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(claimsPrincipal));

        }

        public void UserAuthenticated(User user)
        {
            
            var identity = UserClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void UserIsLoggedOut()
        {

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity UserClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user!= null)
            {
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Nama),
                            new Claim(ClaimTypes.Email, user.Username),
                            new Claim(ClaimTypes.Role, user.Role),
                        };


                 claimsIdentity = new ClaimsIdentity(
                                claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

            }

            return claimsIdentity;
        }


    }
}
