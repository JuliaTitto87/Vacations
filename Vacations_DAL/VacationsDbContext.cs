using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vacations_DomainModel.Models.Identity;

namespace Vacations_DAL
{
    public class VacationsDbContext : IdentityDbContext<User>
    {
        public VacationsDbContext(DbContextOptions<VacationsDbContext> options) : base(options)
        {

        }

        // public DbSet<Feedback> Feedbacks;
        // public DbSet<Order> Orders;
        // ... - use ApplyConfigurationsFromAssembly instead and create configs for entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }



}
