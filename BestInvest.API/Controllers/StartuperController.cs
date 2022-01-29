using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestInvest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StartuperController : ControllerBase
    {
        private readonly IStartuperService startuperService;

        public StartuperController(IStartuperService startuperService)
        {
            this.startuperService = startuperService;
        }

        // GET: api/<StartuperController>
        [HttpGet]
        [Authorize(Policy = "startuper")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StartuperController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<StartuperController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StartuperController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
