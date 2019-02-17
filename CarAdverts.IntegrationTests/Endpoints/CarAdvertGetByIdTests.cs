using CarAdverts.IntegrationTests.Configurations;
using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public class CarAdvertGetByIdTests : CarAdvertTestBase
    {
        public CarAdvertGetByIdTests(CarAdvertsWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_Car_Advert_By_Id_Should_Return_Value()
        {
            var getAllHttpResponse = await Client.GetAsync(Endpoint);

            getAllHttpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(getAllHttpResponse);

            var carAdvertId = carAdverts.First().Id;

            var httpResponse = await Client.GetAsync($"{Endpoint}/{carAdvertId}");

            httpResponse.EnsureSuccessStatusCode();

            var carAdvert = await GetAsInstance(httpResponse);


            carAdvert.Id.Should().Be(carAdvertId);
        }

        [Fact]
        public async Task Get_Car_Advert_By_Id_Should_Return_No_Content()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}/{Guid.Empty}");
            httpResponse.EnsureSuccessStatusCode();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
