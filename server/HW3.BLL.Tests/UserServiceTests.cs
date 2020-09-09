using AutoMapper;
using HW3.Common.DTO;
using HW3.BLL.Services;
using HW3.DAL.Abstracts;
using HW3.DAL.Models;
using HW3.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using HW3.Common;
using FakeItEasy;
using HW3.BLL.ServicesAbstraction;
using System.Collections.Generic;
using HW3.Common.Exeptions;

namespace HW3.BLL.Tests
{
    public class UserServiceTests:IDisposable
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FakeDbContext context;
        private readonly ITaskService taskService;
        private readonly IProjectService projectService;
        public UserServiceTests()
        {
            context = new FakeDbContext();
            _unitOfWork = new UnitOfWork(context);
            _mapper = new MapperConfiguration(c =>
                c.AddProfile<ConfigurationMapper>()).CreateMapper();
            userService = new UserService(_unitOfWork, _mapper);
            taskService = new TaskService(_unitOfWork, _mapper);
            projectService = new ProjectService(_unitOfWork, _mapper);
        }

        public void Dispose()
        {
            userService.Dispose();
        }
        [Fact]
        public async Task CreateUser_WhenNewUser_ThenUsersPlusOne()
        {
            var users = await userService.GetUsers().ConfigureAwait(false);
            UserDTO user = new UserDTO() {FirstName="TestFirstName",LastName="TestLastName"};
            await userService.CreateUser(user).ConfigureAwait(false);
            var usersAfter = await userService.GetUsers().ConfigureAwait(false);
            Assert.Equal(users.Count+1,usersAfter.Count);
        }

       [Fact]//Task 5
       public async Task GetListUserByFirstName_WhenCallListUserByFirstName_ThenEqualType()
       {
            var listUser =await userService.GetListUserByFirstName().ConfigureAwait(false);
            Assert.True(listUser.GetType() == typeof(List<UserDTO>));
       }
        [Theory]
        [InlineData(5)]//Task 6
        public async Task GetInfoAboutLastProjectByUserId_WhenAuthorId2_ThenCountNotFinishedOrCanceledTasks1(int authorId)
        {
            var user = new UserDTO()
            {
                Id = authorId,
                FirstName = "Test",
                LastName = "Test"
            };
            await userService.CreateUser(user).ConfigureAwait(false);
            var project = new ProjectDTO()
            {
                Id = 1,
                Description = "Name",
                Name = "Name"
            };
            await projectService.CreateProject(project).ConfigureAwait(false);
            await taskService.CreateTask(
                new TaskDTO()
                {
                    Id = 4,
                    Description = "Test",
                    PerformerId = authorId,
                    ProjectId = 1
                }).ConfigureAwait(false);
            await taskService.CreateTask(
                new TaskDTO()
                {
                    Id = 5,
                    Description = "Test2",
                    PerformerId = authorId,
                    ProjectId = 1
                }).ConfigureAwait(false);
            await userService.UpdateUser(user).ConfigureAwait(false);

            var result =await userService.GetInfoAboutLastProjectByUserId(authorId).ConfigureAwait(false);
            Assert.Equal(2, result.CountNotFinishedOrCanceledTasks);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        public async Task GetNotFinishedTasksForUser_When_Then(int performerId)
        {
            var user = new UserDTO()
            {
                Id = performerId,
                FirstName = "Name",
                LastName = "LastName"
            };
            var notPerformer = new UserDTO()
            {
                Id = 1,
                FirstName = "Negative",
                LastName = "Negative"
            };
            await userService.CreateUser(user).ConfigureAwait(false);
            await userService.CreateUser(notPerformer).ConfigureAwait(false);

            var project1 = new ProjectDTO() { Id = 1, Name = "Test1", Description = "Test1" };
            var project2 = new ProjectDTO() { Id = 2, Name = "Test2", Description = "Test2" };
            var project3 = new ProjectDTO() { Id = 3, Name = "Test3", Description = "Test3" };
            await projectService.CreateProject(project1).ConfigureAwait(false);
            await projectService.CreateProject(project2).ConfigureAwait(false);
            await projectService.CreateProject(project3).ConfigureAwait(false);

            var taskPositive = new TaskDTO() { Id = 1, Description = "Test", State = 1, PerformerId = performerId, ProjectId = project1.Id };
            var taskPositive2 = new TaskDTO() { Id = 2, Description = "TestPositive", State = 4, PerformerId = performerId, ProjectId = project2.Id };
            var taskNegative = new TaskDTO() { Id = 3, Description = "TestNegative", State = 2, PerformerId = performerId, ProjectId = project1.Id };
            var taskNegative2 = new TaskDTO() { Id = 4, Description = "TestNegative2", State = 1, PerformerId = notPerformer.Id, ProjectId = project3.Id };
            var taskNegative3 = new TaskDTO() { Id = 5, Description = "TestNegative3", ProjectId = project3.Id };
            await taskService.CreateTask(taskPositive).ConfigureAwait(false);
            await taskService.CreateTask(taskPositive2).ConfigureAwait(false);
            await taskService.CreateTask(taskNegative).ConfigureAwait(false);
            await taskService.CreateTask(taskNegative2).ConfigureAwait(false);
            await taskService.CreateTask(taskNegative3).ConfigureAwait(false);

            var result = await userService.GetNotFinishedTasksForUser(performerId).ConfigureAwait(false);//Act

            Assert.Equal(2, result.Count);//Assert
        }
        [Theory]
        [InlineData(10)]
        public async Task GetNotFinishedTasksForUser_WhenUserNotExist_ThenNotFoundExceprion(int performerId)
        {
            await Assert.ThrowsAsync<NotFoundException>(() => userService.GetNotFinishedTasksForUser(performerId)).ConfigureAwait(false);
        }
    }
}
