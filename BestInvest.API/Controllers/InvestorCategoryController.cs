using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/investor/{id}/[controller]")]
    [ApiController]
    [Authorize(Policy = "ForInvestor")]
    public class InvestorCategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public InvestorCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get(int id)
        {
            var res = await categoryService.GetByInvestorIdAsync(id);
            return (res == null) ?
                BadRequest("Investor not found.") :
                Ok(res);
        }
    }
}
