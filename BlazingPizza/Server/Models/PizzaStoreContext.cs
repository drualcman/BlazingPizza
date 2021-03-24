using BlazingPizza.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Server.Models
{
    public class PizzaStoreContext : DbContext
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        public PizzaStoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaTopping>().HasKey(pt => new { pt.ToppingId, pt.PizzaId });
            modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(pt => pt.Toppings);
            modelBuilder.Entity<PizzaTopping>().HasOne(pt => pt.Topping).WithMany();

            modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
        }
    }
}
