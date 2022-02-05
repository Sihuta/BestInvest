using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IProjectService
    {
        Task CreateAsync(ProjectDTO projectDTO);
    }
}
