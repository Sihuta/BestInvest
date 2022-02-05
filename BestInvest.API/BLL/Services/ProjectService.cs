using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;

namespace BestInvest.API.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext dbContext;
        private readonly Mapper mapper;

        public ProjectService(AppDbContext appDbContext, Mapper mapper)
        {
            dbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task CreateAsync(ProjectDTO projectDTO)
        {
            var project = new Project()
            {
                BusinessPlanFilePath = projectDTO.BusinessPlanFilePath,
                Description = projectDTO.Description,
                MoneyCapital = projectDTO.MoneyCapital,
                PaybackPeriod = projectDTO.PaybackPeriod,
                Profitability = projectDTO.Profitability,
                StartCapital = projectDTO.StartCapital,
                TeamId = projectDTO.TeamId,
                Title = projectDTO.Title
            };

            if (project.TeamId == 0)
            {
                project.TeamId = await AddProjectTeamAsync(projectDTO);
            }

            await dbContext.AddAsync(project);
            await dbContext.SaveChangesAsync();

            await AddProjectCategoriesAsync(project.Id, projectDTO.Categories);
        }

        private async Task<int> AddProjectTeamAsync(ProjectDTO projectDTO)
        {
            var teamDTO = projectDTO.Team;
            var team = new Team()
            {
                Name = teamDTO.Name
            };

            await dbContext.AddAsync(team);
            await dbContext.SaveChangesAsync();

            var teamMembers = mapper.Map<TeamMemberDTO, TeamMember>(teamDTO.TeamMembers);
            foreach (var member in teamMembers)
            {
                member.TeamId = team.Id;
            }

            await dbContext.AddRangeAsync(teamMembers);
            await dbContext.SaveChangesAsync();

            return team.Id;
        }

        private async Task AddProjectCategoriesAsync(int projectId, List<CategoryDTO> categoryDTOs)
        {
            var categories = mapper.Map<CategoryDTO, Category>(categoryDTOs);
            var projectCategories = new List<ProjectCategory>();

            foreach (var category in categories)
            {
                projectCategories.Add(new ProjectCategory()
                {
                    CategoryId = category.Id,
                    ProjectId = projectId,
                });
            }

            await dbContext.AddRangeAsync(projectCategories);
            await dbContext.SaveChangesAsync();
        }
    }
}