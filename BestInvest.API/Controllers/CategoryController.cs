using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var categories = await categoryService.GetAsync();
            return Ok(categories);
        }

        [HttpGet("project/{id}")]
        public async Task<ActionResult<List<CategoryDTO>>> GetByProjectId(int id)
        {
            var categories = await categoryService.GetByProjectIdAsync(id);
            return Ok(categories);
        }

        [HttpGet("investor/{id}")]
        public async Task<ActionResult<List<CategoryDTO>>> GetByInvestorId(int id)
        {
            var categories = await categoryService.GetByInvestorIdAsync(id);
            return Ok(categories);
        }
    }
}
