using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Order.Persistence
{
    public class PizzaOrderContext : IdentityDbContext<Employee>
    {
        public PizzaOrderContext(DbContextOptions<PizzaOrderContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Orders> Orders { get; set; }

    }
}
