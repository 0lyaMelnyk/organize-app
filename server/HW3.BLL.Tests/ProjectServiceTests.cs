using AutoMapper;
using FakeItEasy.Sdk;
using HW3.BLL.Services;
using HW3.BLL.ServicesAbstraction;
using HW3.Common;
using HW3.Common.DTO;
using HW3.DAL.Abstracts;
using HW3.DAL.Models;
using HW3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Threading.Tasks;
using HW3.Common.Exeptions;

namespace HW3.BLL.Tests
{
    public class ProjectServiceTests:IDisposable
    {
        private readonly IProjectService projectService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FakeDbContext context;
        private readonly ITaskService taskService;
        private readonly IUserService userService;
        public ProjectServiceTests()
        {
            context = new FakeDbContext();
            _unitOfWork = new UnitOfWork(context);
            _mapper = new MapperConfiguration(c => c.AddProfile<ConfigurationMapper>()).CreateMapper();
            projectService = new ProjectService(_unitOfWork, _mapper);
            taskService = new TaskService(_unitOfWork,_mapper);
            userService = new UserService(_unitOfWork, _mapper);
        }
        public void Dispose()
        {
            projectService.Dispose();
        }
        [Theory]
        [InlineData(1)]//task 1
        public async Task GetCountTasksByUser_WhenAdd2TasksForUser_Then2(int authorId)
        {
            var user = new UserDTO { Id = authorId, FirstName = "TestName", LastName = "LastName" };
            await userService.CreateUser(user).ConfigureAwait(false);
            var project = new ProjectDTO() { Id = 1, Name = "Test", Description = "Test", AuthorId=authorId };
            await projectService.CreateProject(project).ConfigureAwait(false);

            var task1 = new TaskDTO() { Id = 1, Description = "Test1", PerformerId = authorId, ProjectId = project.Id };
            var task2 = new TaskDTO() { Id = 2, Description = "Test2", PerformerId = authorId, ProjectId = project.Id };
            await taskService.CreateTask(task1).ConfigureAwait(false);
            await taskService.CreateTask(task2).ConfigureAwait(false);

            var result = await projectService.GetCountTasksByUser(authorId).ConfigureAwait(false);

            int i = result.ToList().FirstOrDefault().Value;
            Assert.Equal(2, i);
        }
        [Fact]
        public async Task GetCountTasksByUser_WhenProjectListIsEmpty_ThenNotFoundException()
        {
            var user = new UserDTO { Id = 1, FirstName = "TestName", LastName = "LastName" };
            await userService.CreateUser(user).ConfigureAwait(false);
            var task1 = new TaskDTO() { Id = 1, Description = "Test1", PerformerId = 1, ProjectId = 1 };
            var task2 = new TaskDTO() { Id = 2, Description = "Test2", PerformerId = 1, ProjectId = 1 };
            await taskService.CreateTask(task1).ConfigureAwait(false);
            await taskService.CreateTask(task2).ConfigureAwait(false);

            await Assert.ThrowsAsync<NotFoundException>(() => projectService.GetCountTasksByUser(user.Id)).ConfigureAwait(false);
        }
        [Fact]//Task 7
        public async Task GetInfoAboutProjects_WhenNotExistProject_ThenException()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => projectService.GetInfoAboutProjects()).ConfigureAwait(false);
        }
        [Fact]
        public async Task DeleteProject_WhenProjectExist_ThenAsync()
        {
            var project = new ProjectDTO() { Id = 1, Name = "Test", Description = "Test"};
            await projectService.CreateProject(project).ConfigureAwait(false);

            await projectService.DeleteProject(1).ConfigureAwait(false);

            Assert.Empty(await projectService.GetCountTasksByUser(project.Id).ConfigureAwait(false));
        }
    }
}
