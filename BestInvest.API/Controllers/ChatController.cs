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
        public async Task<ActionResult<List<ChatDTO>>> Get(int id)
        {
            var chats = await chatService.GetAsync(id);
            return (chats == null) ?
                BadRequest("Chats of the project are not found.") :
                Ok(chats);
        }

        [HttpGet("{investorId}")]
        public async Task<ActionResult<ChatDTO>> Get(int id, int investorId)
        {
            var chat = await chatService.GetAsync(id, investorId);
            return (chat == null) ?
                BadRequest("Chats if the project with such investor are not found.") :
                Ok(chat);
        }


        [HttpDelete("{chatId}")]
        public async Task<ActionResult<List<ChatDTO>>> Delete(int chatId)
        {
            var res = await chatService.DeleteAsync(chatId);
            return res ? Ok() : BadRequest("Chat not found");
        }
    }
}
