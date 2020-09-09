using HW3.Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HW3.WebAPI.IntegrationTests
{
    public class ProjectsControllerTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public ProjectsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task CreateProject_WhenPost_ThenResponseOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/projects");
            var project = new ProjectDTO() { Name = "Test", Description = "Test" };

            request.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task CreateProject_WhenNotValidData_ThenResponseBadRequest()
        {
           const string projectJson = "{\"AuthorId\":\"no\"}";
           var content = new StringContent(projectJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/projects", content).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task GetInfoAboutProjects_WhenProjectsExist_ThenOk()
        {
            var response = await client.GetAsync("api/projects/tasks").ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
       [Fact]
       public async Task GetCountTasksByUser_WhenUserExist_ThenOK()
        {
            var response = await client.GetAsync("api/projects/counttasks/15").ConfigureAwait(false);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
