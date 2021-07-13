using AdoDotNetProj.Manager.Interface;
using AdoDotNetProj.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Repository.Interface;

namespace AdoDotNetProj.Manager
{
    public class LoginManager:ILoginManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public LoginManager(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthResult> Login(LoginVm loginVm)
        {
            var user = await _userRepository.GetUserAsync(loginVm.Email);
            var result = new AuthResult();
            if (user == null)
            {
                result.Success = false;
                result.Errors.Add("Invalid identity. User not found");
                return result;
            }

            if (loginVm.Password != user.Password)
            {
                result.Success = false;
                result.Errors.Add("Invalid password");
                return result;
            }


            var httpContext = _httpContextAccessor.HttpContext;
            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new("UserName", user.FirstName+" "+user.LastName),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            result.Success = true;
            return result;
        }

    }


    public class AuthResult
    {
        public List<string> Errors = new();
        public bool Success;
    }
}
