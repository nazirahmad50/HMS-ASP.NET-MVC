using HMS.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class SignInManager : SignInManager<HMSUser, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
    {
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(HMSUser user)
    {
        return user.GenerateUserIdentityAsync((UserManager)UserManager);
    }

    public static SignInManager Create(IdentityFactoryOptions<SignInManager> options, IOwinContext context)
    {
        return new SignInManager(context.GetUserManager<UserManager>(), context.Authentication);
    }
}
}
