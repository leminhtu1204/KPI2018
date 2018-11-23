using LunchManagement.Models;
using LunchManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public class MealService : GenericService <Meal>, IMealService
    {
        private readonly IMealRepository mealRepository;
        public MealService(IMealRepository mealRepository) : base(mealRepository)
        {
            this.mealRepository = mealRepository;
        }
    }
}
