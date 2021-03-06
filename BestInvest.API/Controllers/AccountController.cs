using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> Get(int id)
        {
            var res = await accountService.GetAsync(id);
            return (res == null) ?
                BadRequest("User not found.") :
                Ok(res);
        }

        [HttpGet("find/{login}")]
        public async Task<ActionResult<AccountDTO>> FindByLogin(string login)
        {
            var res = await accountService.FindByLoginAsync(login);
            return (res == null) ?
                BadRequest("User not found.") :
                Ok(res);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountDTO accountDTO)
        {
            if (accountDTO == null)
            {
                return BadRequest($"Parameter '{nameof(accountDTO)}' is null");
            }

            var res = await accountService.CreateAsync(accountDTO);
            return res ?
                Ok() : BadRequest("User with such email or login already exists.");
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null)
            {
                return BadRequest($"Parameter '{nameof(changePasswordDTO)}' is null");
            }

            var res = await accountService.ChangePasswordAsync(User, changePasswordDTO);
            return res ?
                Ok() : BadRequest("Something went wrong. Password hasn't been changed.");
        }
    }
}
