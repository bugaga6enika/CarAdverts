using CarAdverts.Application.CarAdvert.Dtos;
using CarAdverts.IntegrationTests.Configurations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public abstract class CarAdvertTestBase : IClassFixture<CarAdvertsWebApplicationFactory<Startup>>
    {
        protected const string Endpoint = "/api/car-adverts";
        protected readonly HttpClient Client;

        public CarAdvertTestBase(CarAdvertsWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        protected virtual async Task<IEnumerable<CarAdvertListDto>> GetAsList(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CarAdvertListDto>>(stringResponse);
        }

        protected virtual async Task<CarAdvertListDto> GetAsInstance(HttpResponseMessage httpResponse)
        {
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CarAdvertListDto>(stringResponse);
        }
    }
}
