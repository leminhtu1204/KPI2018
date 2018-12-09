using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Models
{
    public class Meal : BaseClass
    {
        public string MealName { get; set; }

        public int Price { get; set; }

        public bool IsPrimary { get; set; }
    }
}
