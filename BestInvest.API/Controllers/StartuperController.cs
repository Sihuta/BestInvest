using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ForStartuper")]
    public class StartuperController : ControllerBase
    {
        private readonly IStartuperService startuperService;

        public StartuperController(IStartuperService startuperService)
        {
            this.startuperService = startuperService;
        }

        [HttpGet]
        public async Task<ActionResult<AccountDTO>> GetFullInfo()
        {
            var accountFullInfo = await startuperService.GetFullInfoAsync(User);
            return Ok(accountFullInfo);
        }

        [HttpPut]
        public async Task<IActionResult> EditProfile([FromBody] AccountDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return BadRequest($"Parameter '{nameof(accountDTO)}' is null");
            }

            var res = await startuperService.UpdateAsync(User, accountDTO);
            return res ?
                Ok() : BadRequest("User with such email or login already exists.");
        }
    }
}
