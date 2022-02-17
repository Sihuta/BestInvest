using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IMessageService
    {
        Task CreateAsync(MessageDTO messageDTO);
        Task<bool> RemoveAsync(int id);
    }
}
