using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        [Authorize(Policy = "ForStartuper")]
        public async Task<IActionResult> Add([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                return BadRequest($"Parameter '{nameof(projectDTO)}' is null");
            }

            await projectService.CreateAsync(projectDTO);
            return Ok();
        }
    }
}
