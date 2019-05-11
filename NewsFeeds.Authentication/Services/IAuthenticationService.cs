using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NewsFeeds.Authentication.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LogIn(string email, string password);
        Task LogOut();
        Task<IEnumerable<IdentityError>> Register(string email, string password, string confirmPassword,
            params string[] roles);
    }
}
