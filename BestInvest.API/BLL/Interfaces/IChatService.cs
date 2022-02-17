using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatDTO>> GetAllAsync(int projectId);
        Task<ChatDTO> GetAsync(int chatId);
        Task<bool> UpdateAsync(ChatDTO chatDTO);
        Task CreateAsync(ChatDTO chatDTO);
        Task<bool> RemoveAsync(int chatId);
    }
}
