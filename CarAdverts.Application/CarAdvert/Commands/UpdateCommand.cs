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
        public string FirstRegistration { get; set; }

        public UpdateCommand()
        {
        }

        public UpdateCommand(Guid id, CreateCommand createCommand)
        {
            Id = id;
            Title = createCommand.Title;
            Price = createCommand.Price;
            Fuel = createCommand.Fuel;
            New = createCommand.New;
            Mileage = createCommand.Mileage;
            FirstRegistration = createCommand.FirstRegistration;
        }
    }
}
