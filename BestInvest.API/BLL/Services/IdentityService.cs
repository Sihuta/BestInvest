using BestInvest.API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BestInvest.API.BLL.Services
{
    public class IdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> CreateUserAsync(Account account)
        {
            var user = new IdentityUser { Email = account.Email, UserName = account.Login };
            var result = await userManager.CreateAsync(user, account.Password);

            if (result.Succeeded)
            {
                account.Password = user.PasswordHash;

                await signInManager.SignInAsync(user, false);
                await userManager.AddClaimAsync(user, new Claim("Role", account.Role));
            }
            
            return result.Succeeded;
        }
    }
}
