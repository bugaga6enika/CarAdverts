﻿using CarAdverts.Application.CarAdvert.Commands;
using CarAdverts.Domain.CarAdvert;
using CarAdverts.IntegrationTests.Configurations;
using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.IntegrationTests.Endpoints
{
    public class CarAdvertDeleteTests : CarAdvertTestBase
    {
        public CarAdvertDeleteTests(CarAdvertsWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Delete_Of_The_CarAdvert_Should_Be_Successful()
        {
            var httpResponse = await Client.GetAsync($"{Endpoint}");

            httpResponse.EnsureSuccessStatusCode();

            var carAdverts = await GetAsList(httpResponse);

            var carAdvert = carAdverts.FirstOrDefault();           

            var updateHttpResponse = await Client.DeleteAsync($"{Endpoint}/{carAdvert.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var deletedCarAdvertHttpResponse = await Client.GetAsync($"{Endpoint}/{carAdvert.Id}");

            deletedCarAdvertHttpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Of_The_CarAdvert_Response_Should_Be_Bad_Request()
        {
            var httpResponse = await Client.DeleteAsync($"{Endpoint}/{Guid.Empty}");          

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
