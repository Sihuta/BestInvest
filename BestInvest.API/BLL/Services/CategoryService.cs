using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestInvest.API.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext dbContext;
        private readonly Mapper mapper;

        public CategoryService(AppDbContext appDbContext, Mapper mapper)
        {
            dbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAsync()
        {
            return mapper.Map<Category, CategoryDTO>(await dbContext.Categories.ToListAsync());
        }

        public async Task<List<CategoryDTO>> GetByInvestorIdAsync(int id)
        {
            return await dbContext.InvestorCategories
                .Include(ic => ic.Category)
                .Where(ic => ic.AccountId == id)
                .Select(ic => new CategoryDTO { Id = ic.Category.Id, Name = ic.Category.Name})
                .ToListAsync();
        }

        public async Task<List<CategoryDTO>> GetByProjectIdAsync(int id)
        {
            return await dbContext.ProjectCategories
                .Include(pc => pc.Category)
                .Where(pc => pc.ProjectId == id)
                .Select(pc => new CategoryDTO { Id = pc.Category.Id, Name = pc.Category.Name })
                .ToListAsync();
        }
    }
}
