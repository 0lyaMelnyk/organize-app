using HW3.BLL.Services;
using HW3.Common.DTO;
using HW3.DAL.Repositories;
using HW3.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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
    public class UsersControllerIntegrationTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public UsersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task DeleteUser_WhenUserExist_ThenNoContent()
        {
             var deleteResponse = await client.DeleteAsync($"api/users/{15}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task CreateUser_WhenPost_ThenResponseCreated()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/users");
            var user = new UserDTO() { FirstName = "Test", LastName = "Test" };

            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task DeleteUser_WhenUserNotExist_ThenNotFound()
        {
            var deleteResponse = await client.DeleteAsync($"api/users/{int.MaxValue}");

            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }
        [Fact]
        public async Task CreateUser_WhenNotValidateData_ThenResponseBadRequest()
        {            
            var content = new StringContent("SomeText", Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/users",content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Theory]
        [InlineData(5)]
        public async Task GetNotFinishedTasksForUser_WhenUserAndTaskExist_ThenOk(int performerId)
        {
            var user = new UserDTO() { Id = performerId, FirstName = "Name", LastName = "LastName" };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var project1 = new ProjectDTO() { Id = 1, Name = "Test1", Description = "Test1" };
            var project2 = new ProjectDTO() { Id = 2, Name = "Test2", Description = "Test2" };
            var contentProject1 = new StringContent(JsonConvert.SerializeObject(project1), Encoding.UTF8, "application/json");
            var contentProject2 = new StringContent(JsonConvert.SerializeObject(project2), Encoding.UTF8, "application/json");


            var taskPositive = new TaskDTO() { Id = 1, Description = "Test", State = 1, PerformerId = performerId, ProjectId = 1 };
            var taskPositive2 = new TaskDTO() { Id = 2, Description = "TestPositive", State = 4, PerformerId = performerId, ProjectId = 2 };
            var taskNegative = new TaskDTO() { Id = 3, Description = "TestNegative", State = 2, PerformerId = performerId, ProjectId = 1 };
            var contentTask1 = new StringContent(JsonConvert.SerializeObject(taskPositive), Encoding.UTF8, "application/json");
            var contentTask2 = new StringContent(JsonConvert.SerializeObject(taskPositive2), Encoding.UTF8, "application/json");
            var contentTask3 = new StringContent(JsonConvert.SerializeObject(taskNegative), Encoding.UTF8, "application/json");

            HttpResponseMessage postResponseUser = await client.PostAsync("api/users", content);
            HttpResponseMessage postResponseProject1 = await client.PostAsync("api/projects", contentProject1);
            HttpResponseMessage postResponseProject2 = await client.PostAsync("api/projects", contentProject2);
            HttpResponseMessage postResponseTask1 = await client.PostAsync("api/tasks", contentTask1);
            HttpResponseMessage postResponseTask2 = await client.PostAsync("api/tasks", contentTask2);
            HttpResponseMessage postResponseTask3 = await client.PostAsync("api/tasks", contentTask3);

            var result = await client.GetAsync($"api/users/notFinished/{performerId}");

            result.EnsureSuccessStatusCode();

        }
        [Theory]
        [InlineData(int.MaxValue)]
        public async Task GetNotFinishedTasksForUser_WhenUserNotExist_ThenNotFound(int performerId)
        {
            var result = await client.GetAsync($"api/users/notFinished/{performerId}");
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
       
    }
}
