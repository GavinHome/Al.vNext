using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al.vNext.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DataController
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<string>> Get(long id)
        {
            return "success";
        }
    }
}
