using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.IntegrationTests.Configurations;
using FluentAssertions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public class CarAdvertUpdateTests : CarAdvertTestBase
    {
        public CarAdvertUpdateTests(CarAdvertsWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Update_Of_The_CarAdvert_Should_Be_Successful()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}");

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);

            var carAdvert = carAdverts.FirstOrDefault();

            var createCommand = new CreateCommand
            {
                Title = "Test",
                Price = 33,
                Fuel = FuelType.Gasoline,
                New = false,
                Mileage = 999999999,
                FirstRegistration = "2008-02-12"
            };

            var updateHttpResponse = await Client.PutAsJsonAsync($"{Endpoint}/{carAdvert.Id}", createCommand);

            httpResponse.EnsureSuccessStatusCode();

            var updatedCarAdvertHttpResponse = await Client.GetAsync($"{Endpoint}/{carAdvert.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var updatedCarAdvert = await GetAsInstance(updatedCarAdvertHttpResponse);

            updatedCarAdvert.Id.Should().Be(carAdvert.Id);
            updatedCarAdvert.Title.Should().Be("Test");
        }
    }
}
