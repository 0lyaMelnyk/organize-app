using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW3.Common.DTO;
using HW3.BLL.ServicesAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW3.Common.Exeptions;
using Microsoft.AspNetCore.Cors;

namespace HW3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                return new JsonResult(await _userService.GetUsers().ConfigureAwait(false));
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
                return new JsonResult(await _userService.GetUserById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            try
            {
                await _userService.CreateUser(user).ConfigureAwait(false);
                return Created("api/users",0);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UserDTO user)
        {
            try
            {
                await _userService.UpdateUser(user).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
        [HttpGet("tasks")]//task 5
        public async Task<ActionResult<List<UserDTO>>> GetListUserByFirstName()
        {
            try
            {
                return new JsonResult(await _userService.GetListUserByFirstName().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}/lastProject")]//task 6
        public async Task<ActionResult<AboutLastProjectDTO>> GetInfoAboutLastProjectByUserId(int id)
        {
            try
            {
                return new JsonResult(await _userService.GetInfoAboutLastProjectByUserId(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("notFinished/{id:int}")]
        public async Task<IActionResult> GetNotFinishedTasksForUser(int id)
        {
            try
            {
                return new JsonResult(await _userService.GetNotFinishedTasksForUser(id).ConfigureAwait(false));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


