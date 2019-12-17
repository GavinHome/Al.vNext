using Al.vNext.Core.Extension;
using Al.vNext.Model;
using Al.vNext.Model.Context;
using Al.vNext.Services.Contracts;
using Al.vNext.Services.Implement;
using MassTransit;
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
        private readonly IPublishService _publishService;

        public DataController(IAccountService accountService, AppDbContext dbcontext, IPublishService publishService)
        {
            _accountService = accountService;
            _dbcontext = dbcontext;
            _publishService = publishService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<string>> Get(long id)
        {
            var zero = 0;
            var d = 1 / zero;
            return "success";
        }

        ////[HttpGet]
        ////[Route("[action]")]
        ////public async Task<ActionResult<bool>> GetUser()
        ////{
        ////    var codes = _dbcontext.AsQueryable<Code>().FirstOrDefault();
        ////    return _accountService.Auth("20146348", "1", false, string.Empty);
        ////}

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<bool>> SendMessage()
        {
            var result = await _publishService.Send<SubmitOrder>(new SubmitOrder { OrderId = Guid.NewGuid().ToString(), OrderDate = DateTime.Now, OrderAmount = 1000 }, "submit-order");
            return new ActionResult<bool>(result);
        }
    }

    public class SubmitOrder
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
    }

    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            await Console.Out.WriteLineAsync($"Updating customer: {context.Message.OrderDate}");

            await context.Redeliver(new TimeSpan(100));

            // update the customer address
        }
    }
}
