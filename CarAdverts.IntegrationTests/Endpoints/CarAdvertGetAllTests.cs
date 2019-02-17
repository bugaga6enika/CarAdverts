using CarAdverts.IntegrationTests.Configurations;
using FluentAssertions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public class CarAdvertGetAllTests : CarAdvertTestBase
    {
        public CarAdvertGetAllTests(CarAdvertsWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_All_Car_Adverts_Should_Not_Be_Empty()
        {
            var httpResponse = await Client.GetAsync(Endpoint);

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);

            carAdverts.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Get_All_Car_Adverts_With_Invalid_OrderBy_Query_Should_Return_Bad_Request()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}?orderby=jgdfjgfsdf");

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_All_Car_Adverts_With_OrderBy_Title_Should_Be_Accurate()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}?orderby=title");

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);

            carAdverts.Should().BeInAscendingOrder(x => x.Title);
        }

        [Fact]
        public async Task Get_All_Car_Adverts_With_OrderBy_Price_Desc_Should_Be_Accurate()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}?orderby=price desc");

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);

            carAdverts.Should().BeInDescendingOrder(x => x.Price);
        }

        [Fact]
        public async Task Get_All_Car_Adverts_With_OrderBy_Price_And_RegistrationDate_Desc_Should_Be_Accurate()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}?orderby=price,firstRegistration desc");

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);
            var expectedOrder = carAdverts.OrderByDescending(x => x.Price).ThenByDescending(x => x.FirstRegistration).ToList();

            carAdverts.Should().ContainInOrder(expectedOrder);
        }
    }
}