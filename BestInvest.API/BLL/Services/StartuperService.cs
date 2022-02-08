using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BestInvest.API.BLL.Services
{
    public class StartuperService : IStartuperService
    {
        private readonly AppDbContext dbContext;
        private readonly IdentityService identityService;

        public StartuperService(AppDbContext appDbContext, IdentityService identityService)
        {
            dbContext = appDbContext;
            this.identityService = identityService;
        }

        public async Task<AccountDTO> GetFullInfoAsync(ClaimsPrincipal user)
        {
            var account = await GetCurrentUserAccountAsync(user);

            return new AccountDTO()
            {
                Id = account.Id,
                AccountInfoId = account.AccountInfo.Id,
                
                Login = account.Login,
                Email = account.Email,
                Role = account.Role,

                FullName = account.AccountInfo.FullName,
                DateOfBirth = account.AccountInfo.DateOfBirth,
                LinkedIn = account.AccountInfo.LinkedIn,
                WorkingExperience = account.AccountInfo.WorkingExperience,
            };
        }

        public async Task<bool> UpdateAsync(ClaimsPrincipal user, AccountDTO accountDTO)
        {
            var result = await identityService.UpdateUserAsync(user, accountDTO.Email, accountDTO.Login);
            if (!result)
            {
                return false;
            }
            
            var account = await GetCurrentUserAccountAsync(user);
            var accountInfo = account.AccountInfo;

            account.Email = accountDTO.Email;
            account.Login = accountDTO.Login;

            accountInfo.DateOfBirth = accountDTO.DateOfBirth;
            accountInfo.LinkedIn = accountDTO.LinkedIn;
            accountInfo.WorkingExperience = accountDTO.WorkingExperience;
            accountInfo.FullName = accountDTO.FullName;

            dbContext.Entry(account).State = EntityState.Modified;
            dbContext.Entry(accountInfo).State = EntityState.Modified;
            dbContext.SaveChanges();

            return true;
        }

        private async Task<Account> GetCurrentUserAccountAsync(ClaimsPrincipal user)
        {
            var currentUser = await identityService.GetCurrentUserAsync(user);

            return await dbContext.Accounts
                .Where(a => a.Login == currentUser.UserName)
                .Include(a => a.AccountInfo)
                .FirstOrDefaultAsync();
        }
    }
}
