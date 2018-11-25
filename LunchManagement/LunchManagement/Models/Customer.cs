using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Models
{
    public class Customer : BaseClass
    {
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
    }
}
