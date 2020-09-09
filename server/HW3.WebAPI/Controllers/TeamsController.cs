using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW3.Common.DTO;
using HW3.BLL.ServicesAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTasks()
        {
            try
            {
                return new JsonResult(await _teamService.GetTeams().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return new JsonResult(await _teamService.GetTeamById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeamDTO team)
        {
            try
            {
                await _teamService.CreateTeam(team).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTeam([FromBody] TeamDTO team)
        {
            try
            {
                await _teamService.UpdateTeam(team).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                await  _teamService.DeleteTeam(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]//task 4
        public async Task<ActionResult<List<TeamPlayersDTO>>> GetListUserByTeamAndMoreThenTenYearsOld()
        {
            try
            {
                return new JsonResult(await _teamService.GetListUserByTeamAndMoreThenTenYearsOld().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

