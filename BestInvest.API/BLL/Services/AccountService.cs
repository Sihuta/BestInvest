using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;

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

        public async Task<bool> Create(AccountDTO account)
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

            var result = await identityService.CreateUserAsync(newAccount);
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
