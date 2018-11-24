using LunchManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public interface IMenuService : IGenericService<Menu>
    {
        Task<string> UploadImage(IFormFile file);

        Task<IEnumerable<Menu>> GetMenuByCurrentWeek();
    }
}
