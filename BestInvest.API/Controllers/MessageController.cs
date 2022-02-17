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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageDTO messageDTO)
        {
            if (messageDTO == null)
            {
                return BadRequest($"Parameter '{nameof(messageDTO)}' is null");
            }

            await messageService.CreateAsync(messageDTO);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            bool res = await messageService.RemoveAsync(id);
            return res ? Ok() : BadRequest("Message not found.");
        }
    }
}
