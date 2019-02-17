using CarAdverts.Infrastructure.Contexts;
using static CarAdverts.Domain.CarAdvert.CarAdvert;

namespace CarAdverts.IntegrationTests.Configurations
{
    public static class DataProvider
    {
        public static void Seed(CarAdvertContext dbContext)
        {
            dbContext.CarAdverts.Add(CreateNew("Mazda 6", 35000, Domain.CarAdvert.FuelType.Diesel));
            dbContext.CarAdverts.Add(CreateNew("Opel Astra", 22000, Domain.CarAdvert.FuelType.Diesel));
            dbContext.CarAdverts.Add(CreateNew("Renault Megan", 18000, Domain.CarAdvert.FuelType.Gasoline));
            dbContext.CarAdverts.Add(CreateNew("Skoda Octavia", 18900, Domain.CarAdvert.FuelType.Diesel));
            dbContext.CarAdverts.Add(CreateOld("Toyota Camry", 8000, Domain.CarAdvert.FuelType.Gasoline, 120000, "2015-10-08"));
            dbContext.CarAdverts.Add(CreateOld("Nissan Almera", 6000, Domain.CarAdvert.FuelType.Diesel, 115000, "2014-05-25"));
            dbContext.CarAdverts.Add(CreateOld("Toyota Auris", 10000, Domain.CarAdvert.FuelType.Diesel, 93600, "2016-11-30"));

            dbContext.SaveChanges();
        }
    }
}
