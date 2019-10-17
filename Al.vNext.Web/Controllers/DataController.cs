using Al.vNext.Core.Extension;
using Al.vNext.Model;
using Al.vNext.Model.Context;
using Al.vNext.Services.Contracts;
using Al.vNext.Services.Implement;
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
        private readonly IAccountService _accountService;
        private readonly AppDbContext _dbcontext;
        public DataController(IAccountService accountService, AppDbContext dbcontext)
        {
            _accountService = accountService;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<string>> Get(long id)
        {
            return "success";
        }

        ////[HttpGet]
        ////[Route("[action]")]
        ////public async Task<ActionResult<bool>> GetUser()
        ////{
        ////    var codes = _dbcontext.AsQueryable<Code>().FirstOrDefault();
        ////    return _accountService.Auth("20146348", "1", false, string.Empty);
        ////}
    }
}
