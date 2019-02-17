using CarAdverts.Domain.CarAdvert;
using System;

namespace CarAdverts.Application.CarAdvert.Dtos
{
    public class CarAdvertListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public FuelType Fuel { get; set; }
        public bool New { get; set; }
        public int? Mileage { get; set; }
        public string FirstRegistration { get; set; }
    }
}
