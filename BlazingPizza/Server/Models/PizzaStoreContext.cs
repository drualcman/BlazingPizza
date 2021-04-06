using BlazingPizza.Shared;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Server.Models
{
    public class PizzaStoreContext : ApiAuthorizationDbContext<PizzaStoreUser>
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        public PizzaStoreContext(DbContextOptions options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions) : 
            base(options, operationalStoreOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //invoice OnModelCreating from the base class
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PizzaTopping>().HasKey(pt => new { pt.ToppingId, pt.PizzaId });
            modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(pt => pt.Toppings);
            modelBuilder.Entity<PizzaTopping>().HasOne(pt => pt.Topping).WithMany();

            modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
        }
    }
}
