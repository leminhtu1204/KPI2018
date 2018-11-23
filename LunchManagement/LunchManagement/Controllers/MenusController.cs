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
    public class MenusController : ControllerBase
    {
        private readonly IMenuService menuService;

        public MenusController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result =  await menuService.GetAll();

            return this.Ok(result);
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await menuService.GetById(id);
            return this.Ok(result);
        }

        // POST: api/Meal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Menu menu)
        {
            await menuService.Add(menu);
            return this.Ok();
        }

        // PUT: api/Meal/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Menu menu)
        {
            await menuService.Edit(menu);

            return this.Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Menu menu)
        {
            await menuService.Delete(menu);

            return this.Ok();
        }
    }
}
