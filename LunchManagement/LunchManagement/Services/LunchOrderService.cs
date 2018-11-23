using LunchManagement.Models;
using LunchManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement.Services
{
    public class LunchOrderService : GenericService <LunchOrder>, ILunchOrderService
    {
        private readonly ILunchOrderRepository lunchOrderRepository;
        public LunchOrderService(ILunchOrderRepository lunchOrderRepository) : base(lunchOrderRepository)
        {
            this.lunchOrderRepository = lunchOrderRepository;
        }
    }
}
