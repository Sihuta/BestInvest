using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IDealService
    {
        Task<bool> CreateAsync(DealDTO dealDTO);
        Task<bool> UpdateAsync(DealDTO dealDTO);
        Task<bool> RemoveAsync(int dealId);
        Task<List<DealDTO>> GetAsync(int projectId);
    }
}
