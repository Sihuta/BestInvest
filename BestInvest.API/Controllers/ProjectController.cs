using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ForStartuper")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                return BadRequest($"Parameter '{nameof(projectDTO)}' is null");
            }

            await projectService.CreateAsync(projectDTO);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProjectDTO>> Get(int id)
        {
            var res = await projectService.GetAsync(id);
            return (res == null) ?
                BadRequest("Project not found.") :
                Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectDTO projectDTO)
        {
            if (projectDTO == null)
            {
                return BadRequest($"Parameter '{nameof(projectDTO)}' is null");
            }

            await projectService.UpdateAsync(projectDTO);
            return Ok();
        }
    }
}
