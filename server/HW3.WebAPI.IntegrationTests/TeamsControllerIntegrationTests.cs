using HW3.Common.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HW3.WebAPI.IntegrationTests
{
    public class TeamsControllerIntegrationTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public TeamsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task AddTeam_WhenTeamNotExist_ThenResponseOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/teams");
            const string teamJson = "{\"Name\":\"Name\"}";
            request.Content = new StringContent(teamJson, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task AddTeam_WhenTeamWithNotValideData_ThenBadRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/teams");

            const string teamJson = "{\"CreatedAt\":\"NotBad\"}";
            request.Content = new StringContent(teamJson, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task GetListUserByTeamAndMoreThenTenYearsOld_WhenCorrectQuery_ThenOk()
        {
            var response = await client.GetAsync("api/teams/users").ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

