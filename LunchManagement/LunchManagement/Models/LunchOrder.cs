using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Models
{
    public class LunchOrder : BaseClass
    {
        public List<Meal> Meals { get; set; }

        public Customer Customer { get; set; }

        public bool IsPayed { get; set; }

        public int Total { get; set; }

        public int CustomerId { get; set; }
    }
}
