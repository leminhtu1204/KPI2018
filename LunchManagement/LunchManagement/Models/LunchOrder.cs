using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Models
{
    public class LunchOrder : BaseClass
    {
        public List<Meal> Meals { get; set; }

        public User User { get; set; }

        public bool IsPayed { get; set; }

        public int Total { get; set; }
    }
}
