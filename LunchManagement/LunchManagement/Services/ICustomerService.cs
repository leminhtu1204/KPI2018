using LunchManagement.Models;
using LunchManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<IActionResult> AddAppUserAsync(RegistrationViewModel model);

        Task<IActionResult> CreateToken(LoginViewModel model);
    }
}
