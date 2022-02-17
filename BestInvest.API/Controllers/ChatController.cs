using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/project/{id}/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDTO>>> GetAll(int id)
        {
            var chats = await chatService.GetAllAsync(id);
            return (chats == null) ?
                BadRequest("Chats of the project are not found.") :
                Ok(chats);
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<ChatDTO>> Get(int chatId)
        {
            var chat = await chatService.GetAsync(chatId);
            return (chat == null) ?
                BadRequest("Chat not found.") :
                Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ChatDTO chatDTO)
        {
            await chatService.CreateAsync(chatDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ChatDTO chatDTO)
        {
            var res = await chatService.UpdateAsync(chatDTO);
            return res ? Ok() : BadRequest("Chat not found");
        }

        [HttpDelete("{chatId}")]
        public async Task<ActionResult<List<ChatDTO>>> Delete(int chatId)
        {
            var res = await chatService.RemoveAsync(chatId);
            return res ? Ok() : BadRequest("Chat not found");
        }
    }
}
