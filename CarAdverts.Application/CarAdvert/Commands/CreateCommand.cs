using CarAdverts.Domain.CarAdvert;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CarAdverts.Application.CarAdvert.Commands
{
    public class CreateCommand : IRequest<Dtos.CarAdvertDto>
    { 
        public string Title { get; set; }
        public decimal Price { get; set; }
        public FuelType Fuel { get; set; }
        public bool New { get; set; }
        public int? Mileage { get; set; }
        public string? FirstRegistration { get; set; }
    }
}
