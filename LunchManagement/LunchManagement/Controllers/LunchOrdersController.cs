using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunchManagement.Models;
using LunchManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LunchManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LunchOrdersController : ControllerBase
    {
        private readonly ILunchOrderService lunchOrderService;

        public LunchOrdersController(ILunchOrderService lunchOrderService)
        {
            this.lunchOrderService = lunchOrderService;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result =  await lunchOrderService.GetAll();

            return this.Ok(result);
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await lunchOrderService.GetById(id);
            return this.Ok(result);
        }

        // POST: api/Meal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LunchOrder lunchOrder)
        {
            await lunchOrderService.Add(lunchOrder);
            return this.Ok();
        }

        // PUT: api/Meal/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]LunchOrder lunchOrder)
        {
            await lunchOrderService.Edit(lunchOrder);

            return this.Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(LunchOrder lunchOrder)
        {
            await lunchOrderService.Delete(lunchOrder);

            return this.Ok();
        }
    }
}
