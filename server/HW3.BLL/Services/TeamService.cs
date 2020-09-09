using System;
using HW3.DAL.Repositories;
using HW3.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using HW3.BLL.ServicesAbstraction;
using AutoMapper;
using HW3.Common.DTO;
using HW3.DAL.Abstracts;
using HW3.Common.Exeptions;
using System.Threading.Tasks;

namespace HW3.BLL.Services
{
    public class TeamService:ITeamService
    {
        private readonly IRepository<Team> _repository;
        private readonly IRepository<User> _repositoryUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeamService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository =_unitOfWork.GetRepository<Team>();
            _repositoryUser = _unitOfWork.GetRepository<User>();
            _mapper = mapper;
        }

        public async Task<TeamDTO> GetTeamById(int id)
        {
            var team = await _repository.Get(id).ConfigureAwait(false);
            if (team == null) throw new Exception($"Not found team with id={id}");
            else return _mapper.Map<TeamDTO>(team);
        }

        public async Task<List<TeamDTO>> GetTeams()
        {
            return _mapper.Map<List<TeamDTO>>(await _repository.Get().ConfigureAwait(false));
        }

        public async Task DeleteTeam(int id)
        {
            var user = await _repository.Get(id).ConfigureAwait(false);
            if (user == null)
            {
                throw new Exception($"Not found team with id={id}");
            }
            else
            {
                await DeleteUsersByTeamId(id).ConfigureAwait(false);
                await _repository.Delete(id).ConfigureAwait(false);
            }
        }

        public async Task DeleteTeam(TeamDTO team)
        {
            if (_repository.Get(team.Id) == null)
            {
                throw new Exception($"Not found team with id={team.Id}");
            }
            else
            {
                await DeleteUsersByTeamId(team.Id).ConfigureAwait(false);
                await _repository.Delete(_mapper.Map<Team>(team).Id).ConfigureAwait(false);
            }
        }

        public async Task DeleteUsersByTeamId(int id)
        {
            var users = await _repositoryUser.Get(x => x.TeamId == id).ConfigureAwait(false);
            for (int i = 0; i < users?.Count(); i++)
            {
                var user = await _repositoryUser.Get(users[i].Id).ConfigureAwait(false);
                user.TeamId = null;
            }
        }
        public async Task UpdateTeam(TeamDTO teamDTO)
        {
            var team= await _repository.Get(teamDTO.Id).ConfigureAwait(false);
            if (team == null)
            {
                throw new Exception($"Not found team with id={team.Id}");
            }
            else
            {
                team = _mapper.Map(teamDTO, team);
                await _repository.Update(team).ConfigureAwait(false);
            }
        }

        public async Task CreateTeam(TeamDTO team)
        {
            if (team == null) throw new Exception("You can`t create empty team");
            if (await _repository.Get(team.Id).ConfigureAwait(false) !=null) throw new Exception("This team has alredy exist");
            else await _repository.Create(_mapper.Map<Team>(team)).ConfigureAwait(false);
        }
        //Task 4
        public async Task<List<TeamPlayersDTO>> GetListUserByTeamAndMoreThenTenYearsOld()
        {
            var teams = await _repository.Get().ConfigureAwait(false);
            return teams
                .GroupJoin(
                await _repositoryUser.Get().ConfigureAwait(false),
                t => t.Id,
                u => u.TeamId,
                (team, userList) =>
                new TeamPlayersDTO()
                {
                    Id = team.Id,
                    Name = team.Name,
                    ListUser = _mapper.Map<List<UserDTO>>(userList.Where(user => user.Birthday.Year < 2010)
                    .OrderByDescending(u=>u.RegisteredAt)
                    .ToList())
                }).ToList();
        }
        public async Task AddPlayersToTeam(int userId, int teamId)
        {
            var u = await _repositoryUser.Get(userId).ConfigureAwait(false);
            if (u == null) throw new NotFoundException("Not found this user");
            var team = await _repository.Get(teamId).ConfigureAwait(false);
            if (team == null) throw new NotFoundException("This team not exist");
            u.TeamId = teamId;
            team.UserList?.Add(u);
        }
        public void Dispose()
        {
        }
    }
}
