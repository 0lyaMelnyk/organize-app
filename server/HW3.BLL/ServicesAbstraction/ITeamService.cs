using HW3.Common.DTO;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW3.BLL.ServicesAbstraction
{
    public interface ITeamService:IDisposable
    {
        Task DeleteTeam(int id);
        Task DeleteTeam(TeamDTO team);
        Task UpdateTeam(TeamDTO team);
        Task CreateTeam(TeamDTO team);
        Task<List<TeamDTO>> GetTeams();
        Task<TeamDTO> GetTeamById(int id);
        Task<List<TeamPlayersDTO>> GetListUserByTeamAndMoreThenTenYearsOld();

    }
}
