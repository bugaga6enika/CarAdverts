using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Persistence;
using CarAdverts.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CarAdverts.Infrastructure.Contexts
{
    public class CarAdvertContext : DbContext, IContext<CarAdvert, Guid>
    {
        public DbSet<CarAdvert> CarAdverts { get; set; }

        private readonly IConfiguration _configuration;

        public CarAdvertContext(DbContextOptions<CarAdvertContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarAdvertConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                /*.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)*/;
        }
    }
}