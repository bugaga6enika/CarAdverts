using CarAdverts.Domain.Core.Models;
using CarAdverts.Domain.Core.Validation;
using System;

namespace CarAdverts.Domain.CarAdvert
{
    public class CarAdvert : AggregateRoot<Guid>
    {
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public FuelType Fuel { get; protected set; }
        public bool New { get; protected set; }
        public int? Mileage { get; protected set; }
        public RegistrationDate? FirstRegistration { get; protected set; }

        private CarAdvert() { }

        /// <summary>
        /// Constracts new vehicle
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="fuelType"></param>
        private CarAdvert(string title, decimal price, FuelType fuelType)
        {
            Id = Guid.NewGuid();
            SetTitle(title);
            SetPrice(price);
            SetFuel(fuelType);
            SetAsNew();

            //ToDo: raise create event
        }

        /// <summary>
        /// Constracts old vehicle
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="fuelType"></param>
        /// <param name="mileage"></param>
        /// <param name="firstRegistrationDate"></param>
        private CarAdvert(string title, decimal price, FuelType fuelType, int mileage, string firstRegistrationDate) : this(title, price, fuelType)
        {
            SetAsOld(mileage, firstRegistrationDate);

            //ToDo: raise create event
        }

        /// <summary>
        /// Constracts old vehicle
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="fuelType"></param>
        /// <param name="mileage"></param>
        /// <param name="firstRegistrationDate"></param>
        private CarAdvert(string title, decimal price, FuelType fuelType, int mileage, DateTime firstRegistrationDate) : this(title, price, fuelType)
        {
            SetAsOld(mileage, firstRegistrationDate);

            //ToDo: raise create event
        }

        #region Setters

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ValidationException("Value cannot be null or contains of white spaces", nameof(Title));
            }

            Title = title;
        }

        private void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new ValidationException("Value must be greater then 0", nameof(Price));
            }

            Price = price;
        }

        private void SetFuel(FuelType fuelType)
        {
            if (fuelType == FuelType.NotSpecified)
            {
                throw new ValidationException("Value must be set", nameof(Fuel));
            }

            Fuel = fuelType;
        }

        private void SetAsNew()
        {
            New = true;
            Mileage = null;
            FirstRegistration = RegistrationDate.NotSpecified;
        }

        private void SetAsOld(int mileage, string firstRegistrationDate)
        {
            New = false;
            FirstRegistration = RegistrationDate.Create(firstRegistrationDate);

            if (mileage <= 0)
            {
                throw new ValidationException("Value must be greater then 0", nameof(Mileage));
            }

            Mileage = mileage;
        }

        private void SetAsOld(int mileage, DateTime firstRegistrationDate)
        {
            SetAsOld(mileage, firstRegistrationDate.ToString(RegistrationDate.BaseDateFormat));
        }

        #endregion

        /// <summary>
        /// Core logic for updating a car advert
        /// </summary>
        /// <param name="carAdvertDto"></param>
        public void Update(CarAdvertDto carAdvertDto)
        {
            if (Id == default(Guid))
            {
                throw new ValidationException("Value must not contain default value", nameof(Id));
            }

            if (Title != carAdvertDto.Title)
            {
                SetTitle(carAdvertDto.Title);
            }

            if (Price != carAdvertDto.Price)
            {
                SetPrice(carAdvertDto.Price);
            }

            if (Fuel != carAdvertDto.Fuel)
            {
                SetFuel(carAdvertDto.Fuel);
            }

            if (carAdvertDto.New)
            {
                SetAsNew();
            }
            else
            {
                if (!carAdvertDto.Mileage.HasValue)
                {
                    throw new ValidationException("Value cannot be null", nameof(Mileage));
                }

                SetAsOld(carAdvertDto.Mileage.Value, carAdvertDto.FirstRegistration);
            }

            //ToDo: raise update event
        }

        #region Overrides

        protected override bool AreKeysEquals(Guid self, Guid other)
            => self.Equals(other);

        protected override bool AreEquals(IEntity<Guid> self, IEntity<Guid> other)
        {
            var _this = self as CarAdvert;
            var _other = other as CarAdvert;

            if (_this.Title != _other.Title)
            {
                return false;
            }

            if (_this.Price != _other.Price)
            {
                return false;
            }

            if (_this.Fuel != _other.Fuel)
            {
                return false;
            }

            if (_this.New != _other.New)
            {
                return false;
            }

            if (_this.Mileage != _other.Mileage)
            {
                return false;
            }

            if (_this.FirstRegistration != _other.FirstRegistration)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Fabrics

        public static CarAdvert CreateNew(string title, decimal price, FuelType fuelType)
            => new CarAdvert(title, price, fuelType);

        public static CarAdvert CreateNew(CarAdvertDto carAdvertDto)
            => new CarAdvert(carAdvertDto.Title, carAdvertDto.Price, carAdvertDto.Fuel);

        public static CarAdvert CreateOld(string title, decimal price, FuelType fuelType, int mileage, string firstRegistrationDate)
            => new CarAdvert(title, price, fuelType, mileage, firstRegistrationDate);

        public static CarAdvert CreateOld(CarAdvertDto carAdvertDto)
        {
            if (!carAdvertDto.Mileage.HasValue)
            {
                throw new ValidationException("Value must be greater then 0", nameof(Mileage));
            }

            return new CarAdvert(carAdvertDto.Title, carAdvertDto.Price, carAdvertDto.Fuel, carAdvertDto.Mileage.Value, carAdvertDto.FirstRegistration);
        }

        public static CarAdvert CreateOld(string title, decimal price, FuelType fuelType, int mileage, DateTime firstRegistrationDate)
            => new CarAdvert(title, price, fuelType, mileage, firstRegistrationDate);

        public static CarAdvert Create(CarAdvertDto carAdvertDto)
            => carAdvertDto.New ? CreateNew(carAdvertDto) : CreateOld(carAdvertDto);



        #endregion
    }
}
