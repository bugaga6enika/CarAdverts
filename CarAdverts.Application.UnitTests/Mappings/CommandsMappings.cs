using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Domain.CarAdvert;

namespace CarAdverts.Application.UnitTests.Mappings
{
    public class CommandsMappings : MappingsBase
    {
        [TestCase("Nissan Almera", 15000, FuelType.Diesel, false, 50000, "2012-05-22")]
        [TestCase("Nissan Almera", 15000, FuelType.Diesel, false, null, null)]
        public void CreateCommand_MapToDto_Ok(string title, decimal price, FuelType fuelType, bool isNew, int? mileage, string? registrationDate)
        {
            var createCommand = new CreateCommand
            {
                Title = title,
                Price = price,
                Fuel = fuelType,
                New = isNew,
                Mileage = mileage,
                FirstRegistration = registrationDate
            };

            Func<CarAdvertDto> mappingAction = () => Mapper.Map<CarAdvertDto>(createCommand);

            mappingAction.Should().NotThrow();
            var carAdvertDto = mappingAction.Invoke();

            carAdvertDto.Title.Should().Be(title);
            carAdvertDto.Price.Should().Be(price);
            carAdvertDto.Fuel.Should().Be(fuelType);
            carAdvertDto.New.Should().Be(isNew);
            carAdvertDto.Mileage.Should().Be(mileage);
            carAdvertDto.FirstRegistration.Should().Be(registrationDate);
        }
    }
}
