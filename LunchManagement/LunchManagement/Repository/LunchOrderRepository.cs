using LunchManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Repository
{
    public class LunchOrderRepository : GenericRepository<LunchOrder>, ILunchOrderRepository
    {
        public LunchOrderRepository(ApplicationDbContext applicationDbContext) : base (applicationDbContext)
        {

        }
    }
}
