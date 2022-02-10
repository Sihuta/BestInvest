using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestInvest.API.BLL.Services
{
    public class DealService : IDealService
    {
        private readonly AppDbContext dbContext;
        private readonly Mapper mapper;

        public DealService(AppDbContext appDbContext, Mapper mapper)
        {
            dbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<List<DealDTO>> GetAsync(int projectId)
        {
            return mapper.Map<Deal, DealDTO>(
                await dbContext.Deals.Where(d => d.ProjectId == projectId).ToListAsync());
        }
    }
}
