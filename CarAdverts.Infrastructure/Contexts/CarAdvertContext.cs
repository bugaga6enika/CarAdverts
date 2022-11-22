using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarAdverts.Infrastructure.Contexts
{
    public class CarAdvertContext : DbContext, IContext<CarAdvert, Guid>
    {
        public DbSet<CarAdvert> CarAdverts { get; set; }

        public CarAdvertContext(DbContextOptions<CarAdvertContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarAdvertConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
}