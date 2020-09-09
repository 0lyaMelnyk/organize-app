using AutoMapper;
using HW3.BLL.Services;
using HW3.Common;
using HW3.DAL.Abstracts;
using HW3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HW3.Common.DTO;
using System.Threading.Tasks;
using HW3.BLL.ServicesAbstraction;
using HW3.DAL.Models;
using Umbraco.Core;
using HW3.Common.Exeptions;

namespace HW3.BLL.Tests
{
    public class TasksServiceTests:IDisposable
    {
        private readonly ITaskService taskService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FakeDbContext context;
        private readonly IProjectService projectService;
        private readonly IUserService userService;

        public TasksServiceTests()
        {
            context = new FakeDbContext();
            _unitOfWork = new UnitOfWork(context);
            _mapper = new MapperConfiguration(c => c.AddProfile<ConfigurationMapper>()).CreateMapper();
            taskService = new TaskService(_unitOfWork, _mapper);
            projectService = new ProjectService(_unitOfWork, _mapper);
            userService = new UserService(_unitOfWork, _mapper);
        }
        public void Dispose()
        {
            taskService.Dispose();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void ChangeTaskState_WhenNotFinished_ThenFinished()
        {
            ProjectDTO project = new ProjectDTO { Id = 2, Name="Test", Description="Test"};
            projectService.CreateProject(project);
            TaskDTO task = new TaskDTO() {Id=5, Description = "TastTask", ProjectId = 2, State=1 };
            taskService.CreateTask(task);
            task.State = 2;
            taskService.UpdateTask(task);
            Assert.Equal(2, task.State);
        }

        [Theory]//task 2
        [InlineData(2)]
        [InlineData(1)]
        public async Task GetTasksForUser_WhenIdUser2Or1_ThenCount1(int performerId)
        {
            var user = new UserDTO() { Id = performerId, FirstName = "Test", LastName = "Test" };
            await userService.CreateUser(user).ConfigureAwait(false);

            var project = new ProjectDTO() { Id = 1, Name = "Test", Description = "Test" };
            await projectService.CreateProject(project).ConfigureAwait(false);

            var task = new TaskDTO() { Id = 1,Name="Test", Description="Test",PerformerId = performerId, ProjectId=project.Id };
            await taskService.CreateTask(task).ConfigureAwait(false);

            var res = await taskService.GetTasksForUser(performerId).ConfigureAwait(false);
            Assert.Single(res);
        }

        [Theory]
        [InlineData(5)]//task3
        public async Task GetListFinishedTasksAt2020_WhenCreateFinishedTaskAt2020_ThenListNotEmpty(int performerId)
        {
            var user = new UserDTO()
            {
                Id = performerId,
                FirstName="Name",
                LastName="LastName"
            };
            await userService.CreateUser(user).ConfigureAwait(false);
            var project = new ProjectDTO()
            {
                Id = 10,
                Name = "Test",
                Description = "Test"
            };
            await projectService.CreateProject(project).ConfigureAwait(false);
            var task = new TaskDTO()
            {
                Id = 9, Description = "Test",
                State = 2,
                PerformerId = performerId,
                ProjectId = project.Id,
                FinishedAt = DateTime.Parse("01/01/2020")
            };
            await taskService.CreateTask(task).ConfigureAwait(false);
            var result = await taskService.GetListFinishedTasksAt2020(performerId).ConfigureAwait(false);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(5)]//task3
        public async Task GetListFinishedTasksAt2020_WhenTaskListForThisUserIsEmpty_ThenNotFound(int performerId)
        {
            var user = new UserDTO()
            {
                Id = performerId,
                FirstName = "Name",
                LastName = "LastName"
            };
            await userService.CreateUser(user).ConfigureAwait(false);
            var project = new ProjectDTO()
            {
                Id = 10,
                Name = "Test",
                Description = "Test"
            };
            await projectService.CreateProject(project).ConfigureAwait(false);
            var task = new TaskDTO()
            {
                Id = 9,
                Description = "Test",
                State = 2,
                PerformerId = 6,
                ProjectId = project.Id,
                FinishedAt = DateTime.Parse("01/01/2020")
            };
            await taskService.CreateTask(task).ConfigureAwait(false);
            await Assert.ThrowsAsync<NotFoundException>(() => taskService.GetListFinishedTasksAt2020(performerId)).ConfigureAwait(false);
        }
    }
}
