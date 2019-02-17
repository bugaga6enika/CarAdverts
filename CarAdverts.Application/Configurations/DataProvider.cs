using CarAdverts.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using static CarAdverts.Domain.CarAdvert.CarAdvert;
using static CarAdverts.Domain.CarAdvert.FuelType;

namespace CarAdverts.Application.Configurations
{
    public static class DataProvider
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<CarAdvertContext>())
            {
                context.Database.Migrate();

                if (!context.CarAdverts.Any())
                {
                    context.CarAdverts.Add(CreateNew("Mazda 5", 27000, Diesel));
                    context.CarAdverts.Add(CreateNew("Opel Astra", 22000, Diesel));
                    context.CarAdverts.Add(CreateNew("Renault Megan", 18000, Gasoline));
                    context.CarAdverts.Add(CreateNew("Skoda Octavia", 18900, Diesel));
                    context.CarAdverts.Add(CreateOld("Toyota Camry", 8000, Gasoline, 120000, "2015-10-08"));
                    context.CarAdverts.Add(CreateOld("Nissan Almera", 6000, Diesel, 115000, "2014-05-25"));
                    context.CarAdverts.Add(CreateOld("Toyota Auris", 10000, Diesel, 93600, "2016-11-30"));

                    context.SaveChanges();
                }
            }
        }
    }
}
