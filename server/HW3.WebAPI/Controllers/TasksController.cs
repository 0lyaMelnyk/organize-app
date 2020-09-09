using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW3.Common.DTO;
using HW3.BLL.ServicesAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW3.Common.Exeptions;

namespace HW3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetAllTasks()
        {
            try
            {
                return new JsonResult(await _taskService.GetTasks().ConfigureAwait(false));
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
                return new JsonResult(await _taskService.GetTaskById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskDTO task)
        {
            try
            {
                await _taskService.CreateTask(task).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDTO task)
        {
            try
            {
                await _taskService.UpdateTask(task).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTask(id).ConfigureAwait(false);
                return NoContent();
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
        [HttpGet("forUser/{id:int}")]//task 2
        public async Task<IActionResult> GetTasksForUser(int id)
        {
            try
            {
                return new JsonResult(await _taskService.GetTasksForUser(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("finished2020/{id:int}")]//task 3
        public async Task<IActionResult> GetListFinishedTasksAt2020(int id)
        {
            try
            {
                return new JsonResult(await _taskService.GetListFinishedTasksAt2020(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
