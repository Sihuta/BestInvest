using BestInvest.API.BLL.DTO;

namespace BestInvest.API.BLL.Interfaces
{
    public interface IProjectService
    {
        Task CreateAsync(ProjectDTO projectDTO);
        Task<ProjectDTO> GetAsync(int id);
        Task UpdateAsync(ProjectDTO projectDTO);
    }
}
