using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // POST api/<AccountController>
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] AccountDTO account)
        {
            if (account == null)
            {
                return BadRequest("Parameter 'account' is null");
            }

            var res = await accountService.Create(account);
            return res ?
                Ok() : BadRequest("User with such email already exists.");
        }
    }
}
