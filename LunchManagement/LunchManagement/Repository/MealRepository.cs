using LunchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Repository
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(ApplicationDbContext applicationDbContext) : base (applicationDbContext)
        {

        }
    }
}
