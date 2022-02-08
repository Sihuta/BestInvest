using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task UpdateAsync(ProjectDTO projectDTO)
        {
            var project = await dbContext.Projects.FindAsync(projectDTO.Id);

            if (project != null)
            {
                project.MoneyCapital = projectDTO.MoneyCapital;
                project.StartCapital = projectDTO.StartCapital;
                project.BusinessPlanFilePath = projectDTO.BusinessPlanFilePath;
                project.Description = projectDTO.Description;
                project.PaybackPeriod = projectDTO.PaybackPeriod;
                project.Profitability = projectDTO.Profitability;
                project.Title = projectDTO.Title;

                dbContext.Entry(project).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                await UpdateProjectCategoriesAsync(project.Id, projectDTO.Categories);
                await UpdateProjectTeamAsync(projectDTO.Team);
            }
        }

        public async Task<ProjectDTO> GetAsync(int id)
        {
            var project = await dbContext.Projects
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            var projectDTO = new ProjectDTO()
            {
                Id = id,
                TeamId = project.TeamId,

                BusinessPlanFilePath = project.BusinessPlanFilePath,
                Description = project.Description,
                MoneyCapital = project.MoneyCapital,
                PaybackPeriod = project.PaybackPeriod,
                Profitability = project.Profitability,
                StartCapital = project.StartCapital,
                Title = project.Title
            };

            projectDTO.Team = new TeamDTO()
            {
                Id = project.TeamId,
                Name = project.Team.Name,
                TeamMembers = mapper.Map<TeamMember, TeamMemberDTO>(
                    await dbContext.TeamMembers.Where(tm => tm.TeamId == project.TeamId).ToListAsync())
            };

            projectDTO.Categories = await dbContext.ProjectCategories
                .Include(pc => pc.Category)
                .Where(pc => pc.ProjectId == id)
                .Select(pc => new CategoryDTO { Id = pc.Category.Id, Name = pc.Category.Name })
                .ToListAsync();

            return projectDTO;
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
                project.TeamId = await AddProjectTeamAsync(projectDTO.Team);
            }

            await dbContext.AddAsync(project);
            await dbContext.SaveChangesAsync();

            await AddProjectCategoriesAsync(project.Id, projectDTO.Categories);
        }

        private async Task<int> AddProjectTeamAsync(TeamDTO teamDTO)
        {
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

        private async Task UpdateProjectTeamAsync(TeamDTO teamDTO)
        {
            var team = await dbContext.Teams.FindAsync(teamDTO.Id);
            team.Name = teamDTO.Name;
            dbContext.Entry(team).State = EntityState.Modified;

            var teamMembers = mapper.Map<TeamMemberDTO, TeamMember>(teamDTO.TeamMembers);
            var oldTeamMembers = await dbContext.TeamMembers.Where(tm => tm.TeamId == team.Id).ToListAsync();
            var newTeamMembers = new List<TeamMember>();

            foreach (var tm in teamMembers)
            {
                var member = oldTeamMembers.Find(otm => otm.Id == tm.Id);
                if (member == null)
                {
                    member.TeamId = team.Id;
                    newTeamMembers.Add(member);
                }
                else
                {
                    oldTeamMembers.Remove(member);
                    dbContext.Entry(member).State = EntityState.Modified;
                }
            }

            dbContext.RemoveRange(oldTeamMembers);
            await dbContext.AddRangeAsync(newTeamMembers);
            await dbContext.SaveChangesAsync();
        }

        private async Task AddProjectCategoriesAsync(int projectId, List<CategoryDTO> categories)
        {
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

        private async Task UpdateProjectCategoriesAsync(int projectId, List<CategoryDTO> categories)
        {
            var oldProjectCategories = await dbContext.ProjectCategories
                    .Where(pc => pc.ProjectId == projectId).ToListAsync();

            var newProjectCategories = new List<ProjectCategory>();
            foreach (var category in categories)
            {
                var c = oldProjectCategories.Find(opc => opc.Id == category.Id);
                if (c == null)
                {
                    newProjectCategories.Add(new ProjectCategory()
                    {
                        CategoryId = category.Id,
                        ProjectId = projectId,
                    });
                }
                else
                {
                    oldProjectCategories.Remove(c);
                }
            }

            dbContext.RemoveRange(oldProjectCategories);
            await dbContext.AddRangeAsync(newProjectCategories);
            await dbContext.SaveChangesAsync();
        }
    }
}