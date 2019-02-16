using CarAdverts.Domain.Core.Models;
using System;

namespace CarAdverts.Domain.CarAdvert
{
    public class CarAdvert : AggregateRoot<Guid>
    {
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public bool New { get; protected set; }
        public FuelType Fuel { get; protected set; }
        public int? Mileage { get; protected set; }
        public RegistrationDate FirstRegistration { get; protected set; }

        protected override bool AreKeysEquals(Guid self, Guid other)
            => self.Equals(other);
    }
}
