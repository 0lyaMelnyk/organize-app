using System;
using System.Collections.Generic;
using System.Linq;
using HW3.Common.DTO;
using HW3.BLL.ServicesAbstraction;
using Microsoft.AspNetCore.Mvc;
using HW3.Common.Exeptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace HW3.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAllTasks()
        {
            try
            {
                return new JsonResult(await _projectService.GetProjects());
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
                return new JsonResult(await _projectService.GetProjectById(id).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDTO project)
        {
            try
            {
               await _projectService.CreateProject(project).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDTO project)
        {
            try
            {
                await _projectService.UpdateProject(project).ConfigureAwait(false);
                return Ok();
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteProject(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("tasks")]
        public async Task<IActionResult> GetInfoAboutProjects()
        {
            try
            {
                return new JsonResult(await _projectService.GetInfoAboutProjects().ConfigureAwait(false));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("counttasks/{id:int}")]//task 1
        public async Task<IActionResult> GetCountTasksByUser(int id)
        {
            try
            {
                var result = await _projectService.GetCountTasksByUser(id).ConfigureAwait(false);
                return new JsonResult(result.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
