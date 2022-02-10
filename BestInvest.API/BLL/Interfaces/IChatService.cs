using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatDTO>> GetAsync(int projectId);
        Task<ChatDTO> GetAsync(int projectId, int investorId);
        Task<bool> DeleteAsync(int chatId);
    }
}
