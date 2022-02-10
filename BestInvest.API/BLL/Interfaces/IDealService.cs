using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IDealService
    {
        Task<List<DealDTO>> GetAsync(int projectId);
    }
}
