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

        public async Task<AccountDTO> GetAsync(int id)
        {
            var account = await dbContext.Accounts
                .Include(a => a.AccountInfo)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

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

        public async Task<bool> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordDTO changePasswordDTO)
        {
            var currentUser = await identityService.GetCurrentUserAsync(user);
            var newPassword = await identityService.ChangePasswordAsync(currentUser, changePasswordDTO);

            var result = newPassword != null;
            if (result)
            {
                var account = dbContext.Accounts
                    .Where(a => a.Login == currentUser.UserName)
                    .FirstOrDefault();

                account.Password = newPassword;

                dbContext.Entry(account).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

            return result;
        }

        public async Task<bool> CreateAsync(AccountDTO accountDTO)
        {
            var account = new Account()
            {
                Login = accountDTO.Login,
                Password = accountDTO.Password,
                Email = accountDTO.Email,
                Role = accountDTO.Role,
            };

            var result = await identityService.CreateAsync(account);
            if (result)
            {
                await dbContext.AddAsync(account);
                await dbContext.SaveChangesAsync();

                var accountInfo = new AccountInfo()
                {
                    FullName = accountDTO.FullName,
                    DateOfBirth = accountDTO.DateOfBirth,
                    WorkingExperience = accountDTO.WorkingExperience,
                    LinkedIn = accountDTO.LinkedIn,

                    AccountId = account.Id
                };

                await dbContext.AddAsync(accountInfo);
                await dbContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task<AccountDTO> FindByLoginAsync(string login)
        {
            var account = await dbContext.Accounts
                .Include(a => a.AccountInfo)
                .Where(a => a.Login == login)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }

            return new AccountDTO()
            {
                Id = account.Id,
                Email = account.Email,
                Login = login,
                Role = account.Role,

                AccountInfoId = account.AccountInfo.Id,
                DateOfBirth = account.AccountInfo.DateOfBirth,
                FullName = account.AccountInfo.FullName,
                LinkedIn = account.AccountInfo.LinkedIn,
                WorkingExperience = account.AccountInfo.WorkingExperience
            };
        }
    }
}
