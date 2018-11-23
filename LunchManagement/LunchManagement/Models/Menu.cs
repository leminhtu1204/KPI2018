using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Models
{
    public class Menu : BaseClass
    {
        public string MenuImage { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
