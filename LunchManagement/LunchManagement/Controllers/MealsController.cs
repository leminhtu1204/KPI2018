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
    public class MealsController : ControllerBase
    {
        private readonly IMealService mealService;

        public MealsController(IMealService mealService)
        {
            this.mealService = mealService;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result =  await mealService.GetAll();

            return this.Ok(result);
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mealService.GetById(id);
            return this.Ok(result);
        }

        // POST: api/Meal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Meal meal)
        {
            await mealService.Add(meal);
            return this.Ok();
        }

        // PUT: api/Meal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Meal meal)
        {
            await mealService.Edit(meal);

            return this.Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Meal meal)
        {
            await mealService.Delete(meal);

            return this.Ok();
        }
    }
}
