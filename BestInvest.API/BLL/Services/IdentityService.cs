using BestInvest.API.BLL.DTO;
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

        public async Task<bool> UpdateUserAsync(ClaimsPrincipal user, string email, string login)
        {
            var currentUser = await GetCurrentUserAsync(user);

            currentUser.Email = email;
            currentUser.UserName = login;

            var result = await userManager.UpdateAsync(currentUser);
            return result.Succeeded;
        }

        public async Task<string> ChangePasswordAsync(IdentityUser user, ChangePasswordDTO changePasswordDTO)
        {
            var result = await userManager
                .ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

            string newPasswordHash = null;
            if (result.Succeeded)
            {
                newPasswordHash = user.PasswordHash;
            }

            return newPasswordHash;
        }

        public async Task<IdentityUser> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            return await userManager.GetUserAsync(user);
        }

        public async Task<bool> CreateAsync(Account account)
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
