using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAsync();
        Task<List<CategoryDTO>> GetByProjectIdAsync(int id);
        Task<List<CategoryDTO>> GetByInvestorIdAsync(int id);
    }
}
