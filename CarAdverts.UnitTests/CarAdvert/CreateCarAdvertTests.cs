using CarAdverts.Domain.CarAdvert;
using CarAdverts.Domain.Core.Validation;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CarAdverts.UnitTests.CarAdvert
{
    public class CreateCarAdvertTests
    {
        [TestCase("Nissan Almera", 15000, FuelType.Diesel)]
        [TestCase("Mazda 3", 12000, FuelType.Diesel)]
        public void CarAdvert_For_New_Vehicle_Should_Contain_Valid_Id(string title, decimal price, FuelType fuelType)
        {
            var carAdvert = Domain.CarAdvert.CarAdvert.CreateNew(title, price, fuelType);

            var isGuidValid = carAdvert.Id != default(Guid) && carAdvert.Id != Guid.Empty;

            isGuidValid.Should().Be(true);
        }

        [TestCase("", 15000, FuelType.Diesel)]
        [TestCase("   ", 12000, FuelType.Diesel)]
        [TestCase(null, 15000, FuelType.Diesel)]
        [TestCase("Mazda 3", -12000, FuelType.Diesel)]
        [TestCase("Nissan Almera", 0, FuelType.Diesel)]
        [TestCase("Mazda 3", 12000, FuelType.NotSpecified)]
        [TestCase("Mazda 3", 12000, null)]
        [TestCase("Mazda 3", null, FuelType.Diesel)]
        public void CarAdvert_For_New_Vehicle_Should_Throw_Validation_Exception(string title, decimal price, FuelType fuelType)
        {
            Action carAdvertCreateAction = () => Domain.CarAdvert.CarAdvert.CreateNew(title, price, fuelType);

            carAdvertCreateAction.Should().ThrowExactly<ValidationException>();
        }

        [TestCase("Nissan Almera", 15000, FuelType.Diesel, 50000, "2012-05-22")]
        [TestCase("Mazda 3", 12000, FuelType.Diesel, 34789, "2014-10-05")]
        public void CarAdvert_For_Old_Vehicle_Should_Contain_Valid_Id(string title, decimal price, FuelType fuelType, int mileage, string firstRegistrationDate)
        {
            var carAdvert = Domain.CarAdvert.CarAdvert.CreateOld(title, price, fuelType, mileage, firstRegistrationDate);

            var isGuidValid = carAdvert.Id != default(Guid) && carAdvert.Id != Guid.Empty;

            isGuidValid.Should().Be(true);
        }

        [TestCase("Nissan Almera", 15000, FuelType.Diesel, 50000, null)]
        [TestCase("Mazda 3", 12000, FuelType.Diesel, 34789, "2014-13-05")]
        [TestCase("Nissan Almera", 15000, FuelType.Diesel, 0, "2012-05-22")]
        [TestCase("Mazda 3", 12000, FuelType.Diesel, null, "2014-10-05")]
        [TestCase("Nissan Almera", 15000, null, 50000, "2012-05-22")]
        [TestCase("Mazda 3", 12000, FuelType.Diesel, 34789, "")]
        public void CarAdvert_For_Old_Vehicle_Should_Throw_Validation_Exception(string title, decimal price, FuelType fuelType, int mileage, string firstRegistrationDate)
        {
            Action carAdvertCreateAction = () => Domain.CarAdvert.CarAdvert.CreateOld(title, price, fuelType, mileage, firstRegistrationDate);

            carAdvertCreateAction.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void CarAdvert_For_Old_Vehicle_Should_Throw_Validation_Exception()
        {
            var carAdvertDto = new CarAdvertDto
            {
                Title = "Audi A8",
                Fuel = FuelType.Diesel,
                New = false,
                FirstRegistration = "2017-12-05"
            };

            Action carAdvertCreateAction = () => Domain.CarAdvert.CarAdvert.CreateOld(carAdvertDto);

            carAdvertCreateAction.Should().ThrowExactly<ValidationException>();
        }

        [Test]
        public void CarAdvert_Created_From_Dto_Should_Be_Old()
        {
            var carAdvertDto = new CarAdvertDto
            {
                Title = "Audi A8",
                Fuel = FuelType.Diesel,
                Price = 25550,
                New = false,
                Mileage = 34500,
                FirstRegistration = "2017-12-05"
            };

            var carAdvert = Domain.CarAdvert.CarAdvert.Create(carAdvertDto);

            var isCarOld = carAdvert.New == false;

            isCarOld.Should().Be(true);
        }

        [Test]
        public void CarAdvert_Created_From_Dto_Should_Be_New()
        {
            var carAdvertDto = new CarAdvertDto
            {
                Title = "Audi A8",
                Fuel = FuelType.Diesel,
                Price = 25550,
                New = true,
                Mileage = 34500,
                FirstRegistration = "2017-12-05"
            };

            var carAdvert = Domain.CarAdvert.CarAdvert.Create(carAdvertDto);

            var isCarNew = carAdvert.New == true;
            var isMileageNull = !carAdvert.Mileage.HasValue;
            var isFirstRegistrationDateIsNotSpecified = carAdvert.FirstRegistration == RegistrationDate.NotSpecified;

            isCarNew.Should().Be(true);
            isMileageNull.Should().Be(true);
            isFirstRegistrationDateIsNotSpecified.Should().Be(true);
        }
    }
}
