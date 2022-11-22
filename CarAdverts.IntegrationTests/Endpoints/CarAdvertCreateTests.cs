using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.IntegrationTests.Configurations;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public class CarAdvertCreateTests : CarAdvertTestBase
    {
        public CarAdvertCreateTests(CarAdvertsWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("BMW 3 series", 22000, FuelType.Diesel)]
        public async Task Creation_Of_The_CarAdvert_For_New_Car_Should_Be_Successful(string title, decimal price, FuelType fuel)
        {
            var createCarAdvertCommand = new CreateCommand
            {
                Title = title,
                Price = price,
                Fuel = fuel,
                New = true
            };

            var httpResponse = await Client.PostAsJsonAsync($"{Endpoint}", createCarAdvertCommand);

            httpResponse.EnsureSuccessStatusCode();

            var carAdvert = await GetAsInstance(httpResponse);

            carAdvert.Title.Should().Be(title);
            carAdvert.Price.Should().Be(price);
            carAdvert.Fuel.Should().Be(fuel);
            carAdvert.New.Should().Be(true);
            carAdvert.Id.Should().NotBe(default(Guid));
        }

        [Theory]
        [InlineData("BMW 3 series", 22000, FuelType.Diesel, 38000, "2017-10-25")]
        [InlineData("Audi A5", 38000, FuelType.Gasoline, 64000, "20180203")]
        public async Task Creation_Of_The_CarAdvert_For_Old_Car_Should_Be_Successful(string title, decimal price, FuelType fuel, int mileage, string firstRegistration)
        {
            var createCarAdvertCommand = new CreateCommand
            {
                Title = title,
                Price = price,
                Fuel = fuel,
                New = false,
                Mileage = mileage,
                FirstRegistration = firstRegistration
            };

            var httpResponse = await Client.PostAsJsonAsync($"{Endpoint}", createCarAdvertCommand);

            httpResponse.EnsureSuccessStatusCode();

            var carAdvert = await GetAsInstance(httpResponse);

            carAdvert.Title.Should().Be(title);
            carAdvert.Price.Should().Be(price);
            carAdvert.Fuel.Should().Be(fuel);
            carAdvert.New.Should().Be(false);
            carAdvert.Mileage.Should().Be(mileage);
            RegistrationDate.Create(carAdvert.FirstRegistration).Should().Be(RegistrationDate.Create(firstRegistration));
            carAdvert.Id.Should().NotBe(default(Guid));
        }

        [Theory]
        [InlineData("BMW 3 series", 22000, FuelType.Diesel, false, 38000, "2017-10-33")]
        [InlineData("BMW 3 series", 22000, FuelType.Diesel, false, 0, "2017-10-02")]
        [InlineData("BMW 3 series", 0, FuelType.Diesel, true, 38000, "2017-10-14")]
        [InlineData(null, 22000, FuelType.Diesel, false, 38000, null)]
        public async Task Create_CarAdvert_Response_Should_Be_Bad_Request(string title, decimal price, FuelType fuel, bool isNew, int mileage, string firstRegistration)
        {
            var createCarAdvertCommand = new CreateCommand
            {
                Title = title,
                Price = price,
                Fuel = fuel,
                New = false,
                Mileage = mileage,
                FirstRegistration = firstRegistration
            };

            var httpResponse = await Client.PostAsJsonAsync($"{Endpoint}", createCarAdvertCommand);

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
