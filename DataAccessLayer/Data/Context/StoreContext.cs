using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.DataAccessLayer.Data.Context
{
    public class StoreContext : IdentityDbContext<User>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                            .HasMany(o => o.orderItems)
                            .WithOne(oi => oi.Order)
                            .HasForeignKey(oi => oi.OrderId); 

        }

    }
}
