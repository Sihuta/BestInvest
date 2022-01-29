using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;

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

        public Task<AccountDTO> GetFullInfo()
        {
            throw new NotImplementedException();
        }
    }
}
