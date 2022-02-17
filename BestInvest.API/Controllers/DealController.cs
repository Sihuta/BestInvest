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
            return (deals == null) ?
                BadRequest("Deals of the project are not found.") :
                Ok(deals);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DealDTO dealDTO)
        {
            if (dealDTO == null)
            {
                return BadRequest($"Parameter '{nameof(dealDTO)}' is null");
            }

            bool res = await dealService.CreateAsync(dealDTO);
            return res ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] DealDTO dealDTO)
        {
            if (dealDTO == null)
            {
                return BadRequest($"Parameter '{nameof(dealDTO)}' is null");
            }

            bool res = await dealService.UpdateAsync(dealDTO);
            return res ? Ok() : BadRequest();
        }

        [HttpDelete("dealId")]
        public async Task<IActionResult> Delete(int dealId)
        {
            bool res = await dealService.RemoveAsync(dealId);
            return res ? Ok() : BadRequest("Deal not found.");
        }
    }
}
