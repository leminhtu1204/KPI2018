using LunchManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchManagement
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<LunchOrder> LunchOrders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
