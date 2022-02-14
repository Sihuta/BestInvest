using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/project/{id}/[controller]")]
    [ApiController]
    [Authorize(Policy = "ForStartuper")]
    public class ProjectCategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public ProjectCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get(int id)
        {
            var res = await categoryService.GetByProjectIdAsync(id);
            return (res == null) ?
                BadRequest("Project not found.") :
                Ok(res);
        }
    }
}
