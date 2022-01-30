using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] AccountDTO account)
        {
            if (account == null)
            {
                return BadRequest($"Parameter '{nameof(account)}' is null");
            }

            var res = await accountService.CreateAsync(account);
            return res ?
                Ok() : BadRequest("User with such email already exists.");
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
