using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BestInvest.API.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext dbContext;
        private readonly IdentityService identityService;

        public AccountService(AppDbContext appDbContext, IdentityService identityService)
        {
            dbContext = appDbContext;
            this.identityService = identityService;
        }

        public async Task<bool> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDTO changePasswordDTO)
        {
            var currentUser = await identityService.GetCurrentUserAsync(user);
            var newPassword = await identityService.ChangePasswordAsync(currentUser, changePasswordDTO);

            var result = newPassword != null;
            if (result)
            {
                var account = dbContext.Accounts
                    .Where(a => a.Email == currentUser.Email)
                    .FirstOrDefault();

                account.Password = newPassword;

                dbContext.Entry(account).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

            return result;
        }

        public async Task<bool> CreateAsync(AccountDTO account)
        {
            if (dbContext.Accounts.Where(a => a.Email == account.Email).Any())
            {
                return false;
            }

            var newAccount = new Account()
            {
                Login = account.Login,
                Password = account.Password,
                Email = account.Email,
                Role = account.Role,
            };
            var accountInfo = new AccountInfo()
            {
                FullName = account.FullName,
                DateOfBirth = account.DateOfBirth,
                WorkingExperience = account.WorkingExperience,
                LinkedIn = account.LinkedIn,
            };

            var result = await identityService.CreateAsync(newAccount);
            if (result)
            {
                await dbContext.AddAsync(newAccount);
                await dbContext.AddAsync(accountInfo);
                await dbContext.SaveChangesAsync();
            }

            return result;
        }
    }
}
