using AutoMapper;
using HW3.BLL.Services;
using HW3.BLL.ServicesAbstraction;
using HW3.Common;
using HW3.Common.DTO;
using HW3.Common.Exeptions;
using HW3.DAL.Abstracts;
using HW3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace HW3.BLL.Tests
{
    public class TeamServiceTests:IDisposable
    {
        private readonly TeamService teamService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly FakeDbContext context = new FakeDbContext();
        private readonly IUserService userService;
        public TeamServiceTests()
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = new MapperConfiguration(c =>
            c.AddProfile<ConfigurationMapper>()).CreateMapper();
            teamService = new TeamService(_unitOfWork, _mapper);
            userService = new UserService(_unitOfWork, _mapper);
        }
        public void Dispose()
        {
            teamService.Dispose();
        }

        [Theory]
        [InlineData(2,5)]
        [InlineData(25, 53)]
        public async Task AddPlayersToTeam_WhenUserId2TeamId5_ThenTeamId5(int userId,int teamId)
        {
            var team = new TeamDTO() {Id=teamId, Name = "TestName" };
            await teamService.CreateTeam(team).ConfigureAwait(false);

            var user = new UserDTO() { Id = userId, FirstName = "FirstName", LastName = "LastName" };
            await userService.CreateUser(user).ConfigureAwait(false);

            var count =await teamService.GetTeamById(team.Id).ConfigureAwait(false);
            await teamService.AddPlayersToTeam(userId, team.Id).ConfigureAwait(false);
            var countAfter = await teamService.GetTeamById(team.Id).ConfigureAwait(false);
            Assert.Equal(count.UserList?.Count + 1,countAfter.UserList.Count);
        }
        [Theory]
        [InlineData(25, 53)]
        public async Task AddPlayersToTeam_WhenUserNotExist_ThenNotFoundException(int userId, int teamId)
        {
            var team = new TeamDTO() { Id = teamId, Name = "TestName" };
            await teamService.CreateTeam(team).ConfigureAwait(false);
            await Assert.ThrowsAsync<NotFoundException>(()=>teamService.AddPlayersToTeam(userId, team.Id)).ConfigureAwait(false);
        }
        [Fact]//Task 4
        public async Task GetListUserByTeamAndMoreThenTenYearsOld_WhenUserRregisterNowAndTeamIdMinimum_ThenUserIsFirst()
        {
            var team1 = new TeamDTO() { Id = 1, Name = "Team1" };
            var team2 = new TeamDTO() { Id = 2, Name = "Team2" };
            await teamService.CreateTeam(team1).ConfigureAwait(false);
            await teamService.CreateTeam(team2).ConfigureAwait(false);

            var user1 = new UserDTO() { Id=1, FirstName="Test", LastName="Test",Birthday = DateTime.Parse("2/10/2009"), TeamId=1, RegisteredAt=DateTime.Parse("2/10/2020") };
            var user2 = new UserDTO() { Id = 2, FirstName = "Test", LastName = "Test", Birthday = DateTime.Parse("2/10/2008"), TeamId = 2, RegisteredAt = DateTime.Parse("2/10/1019") };
            var user3 = new UserDTO() { Id = 3, FirstName = "Test", LastName = "Test", Birthday = DateTime.Parse("2/10/2009"), TeamId = 1, RegisteredAt = DateTime.Parse("5/11/2020") };
            await userService.CreateUser(user1).ConfigureAwait(false);
            await userService.CreateUser(user2).ConfigureAwait(false);
            await userService.CreateUser(user3).ConfigureAwait(false);

            await teamService.AddPlayersToTeam(user1.Id,1).ConfigureAwait(false);
            await teamService.AddPlayersToTeam(user2.Id, 2).ConfigureAwait(false);
            await teamService.AddPlayersToTeam(user3.Id, 1).ConfigureAwait(false);

            var teamPlayers =await teamService.GetListUserByTeamAndMoreThenTenYearsOld().ConfigureAwait(false);
            Assert.Equal(user3.Id, teamPlayers[0].ListUser[0].Id);
        }
        [Fact]
        public async Task CreateTeam_WhenTeamExist_ThenException()
        {
            var team = new TeamDTO() { Id = 15, Name = "Team" };
            await teamService.CreateTeam(team).ConfigureAwait(false);
            await Assert.ThrowsAsync<Exception>(() => teamService.CreateTeam(team)).ConfigureAwait(false);
        }
    }
}
