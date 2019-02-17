using CarAdverts.Domain.CarAdvert;
using MediatR;
using System;

namespace CarAdverts.Application.CarAdvert.Commands
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public FuelType Fuel { get; set; }
        public bool New { get; set; }
        public int? Mileage { get; set; }
        public string FirstRegistrationDate { get; set; }
    }
}
