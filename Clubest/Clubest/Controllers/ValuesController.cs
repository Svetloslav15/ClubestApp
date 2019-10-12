namespace Clubest.Controllers
{
    using System.Collections.Generic;
    using Clubest.Data;
    using Clubest.Data.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ClubestDbContext dbContext;

        public ValuesController(ClubestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            return this.dbContext.Posts;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}