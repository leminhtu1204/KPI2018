using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LunchManagement.Models;
using LunchManagement.Services;
using Microsoft.AspNetCore.Hosting;
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
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result =  await menuService.GetMenuByCurrentWeek();

            return this.Ok(result);
        }
        
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

        [HttpPost, DisableRequestSizeLimit]
        [Route("uploading")]
        public IActionResult UploadImage()
        {
            var file = Request.Form.Files[0];
            var result = menuService.UploadImage(file).Result;

            return Ok(result);
        }
    }
}
