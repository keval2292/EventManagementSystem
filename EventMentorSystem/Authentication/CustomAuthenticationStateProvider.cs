using EMS.DB.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventMentorSystem.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var userSessionStorageResult = await _sessionStorage.GetAsync<User>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim(ClaimTypes.Sid, userSession.Id.ToString()),
                    new Claim(ClaimTypes.Role, userSession.Userrole)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        //public async Task UpdateAuthenticationState(UserSession userSession)
        //{
        //    ClaimsPrincipal claimsPrincipal;

        //    if (userSession != null)
        //    {
        //        await _sessionStorage.SetAsync("UserSession", userSession);
        //        claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Email, userSession.UserEmail),
        //            new Claim(ClaimTypes.Sid, userSession.Id.ToString()),
        //            new Claim(ClaimTypes.Role, userSession.Role)
        //        }));
        //    }
        //    else
        //    {
        //        await _sessionStorage.DeleteAsync("UserSession");
        //        claimsPrincipal = _anonymous;
        //    }

        //    //NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        //}

        public async Task UpdateAuthenticationState(User userAccount)
        {

            if (userAccount != null)
            {
                await _sessionStorage.SetAsync("UserSession", userAccount);
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
            }

            var identity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Sid, userAccount.Id.ToString()),
                    new Claim(ClaimTypes.Role, userAccount.Userrole)
                });

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
