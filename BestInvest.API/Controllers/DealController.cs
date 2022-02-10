using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/project/{id}/[controller]")]
    [ApiController]
    [Authorize]
    public class DealController : ControllerBase
    {
        private readonly IDealService dealService;

        public DealController(IDealService dealService)
        {
            this.dealService = dealService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DealDTO>>> Get(int id)
        {
            var deals = await dealService.GetAsync(id);
            return (deals == null) ? BadRequest() : Ok(deals);
        }
    }
}
