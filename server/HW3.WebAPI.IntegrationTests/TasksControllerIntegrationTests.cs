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
    public class TasksControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public TasksControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task DeleteTask_WhenUserExist_ThenNoContent()
        {
         
            var deleteResponse = await client.DeleteAsync($"api/tasks/{15}");

          
            deleteResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task DeleteTask_WhenNotExistTask_ThenNotFound()
        {
            var deleteResponse = await client.DeleteAsync($"api/tasks/{int.MaxValue}");

            //Assert
            //Assert.False();
            Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);

        }
      
    }
}
