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
using Microsoft.EntityFrameworkCore;
namespace HW3.BLL.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IRepository<Team> teamRepository;
        private readonly IRepository<User> userRepository;

        public ProjectService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Project>();
            taskRepository = _unitOfWork.GetRepository<TaskModel>();
            userRepository = _unitOfWork.GetRepository<User>();
            teamRepository = _unitOfWork.GetRepository<Team>();
            _mapper = mapper;
        }

        public async Task<ProjectDTO> GetProjectById(int id)
        {
            if (await _repository.Get(id).ConfigureAwait(false) == null) throw new Exception($"Not found project with id={id}");
            else return  _mapper.Map<ProjectDTO>(await _repository.Get(id).ConfigureAwait(false));
        }

        public async Task<List<ProjectDTO>> GetProjects()
        {
            return _mapper.Map<List<ProjectDTO>>(await _repository.Get().ConfigureAwait(false));
        }

        public async Task DeleteProject(int id)
        {
            if (await _repository.Get(id).ConfigureAwait(false) == null)
            {
                throw new Exception($"Not found project with id={id}");
            }
            else
            {
                var tasks = await taskRepository.Get(x => x.ProjectId == id).ConfigureAwait(false);
                for (int i = 0; i < tasks.Count; i++)
                {
                    await taskRepository.Delete(tasks[i].Id).ConfigureAwait(false);
                }
                await _repository.Delete(id).ConfigureAwait(false);
            }
        }

        public async Task DeleteProject(ProjectDTO project)
        {
            if (await _repository.Get(project.Id).ConfigureAwait(false) == null)
            {
                throw new Exception($"Not found project with id={project.Id}");
            }
            else
            {
                await _repository.Delete(_mapper.Map<Project>(project).Id).ConfigureAwait(false);
            }
        }

        public async Task DeleteTasksByProjectId(int id)
        {
            var tasks = await taskRepository.Get(x => x.ProjectId == id).ConfigureAwait(false);
            for (int i = 0; i < tasks?.Count(); i++)
            {
                await taskRepository.Delete(tasks[i].Id).ConfigureAwait(false);
            }
        }
        public async Task UpdateProject(ProjectDTO project)
        {
            if (await _repository.Get(project.Id).ConfigureAwait(false) == null)
            {
                throw new Exception($"Not found project with id={project.Id}");
            }
            else
            {
                var projectEntity = await _repository.Get(project.Id).ConfigureAwait(false);
                projectEntity = _mapper.Map(project, projectEntity);
                await _repository.Update(projectEntity).ConfigureAwait(false);
            }
        }

        public async Task CreateProject(ProjectDTO project)
        {
            if (project == null) throw new Exception("You can`t create empty project");
            else await _repository.Create(_mapper.Map<Project>(project)).ConfigureAwait(false);
        }
        //Task 1
        public async Task<Dictionary<int, int>> GetCountTasksByUser(int authorId)
        {
            var count = await _repository.Get().ConfigureAwait(false);
            if (count.Count== 0) throw new NotFoundException("Project list is empty");
            var tasks = await taskRepository.Get().ConfigureAwait(false);
            var result = count.Where(x => x.AuthorId == authorId)
                .GroupJoin(tasks, p => p.Id,
                t => t.ProjectId,
                (p, t) => new
                {
                    projectId = p.Id,
                    taskListCount = t.Count()
                });
            return result.ToDictionary(g => g.projectId, g => g.taskListCount);
        }
        //task 7
        public async Task<List<AboutProjectDTO>> GetInfoAboutProjects()
        {
            var projectList = await _repository.Get().ConfigureAwait(false);
            if (projectList.Count == 0) throw new NotFoundException("Project list is empty");

            var teamList = await teamRepository.Get().ConfigureAwait(false);

            var userList = await userRepository.Get().ConfigureAwait(false);
            var taskList = await taskRepository.Get().ConfigureAwait(false);
            var aboutProject = teamList
                .GroupJoin(
                userList,
                t => t.Id,
                u => u.TeamId,
                (team, u) =>
                {
                    team.UserList = u.ToList();
                    return team;
                })
                .Join(
                projectList,
                t => t.Id,
                p => p.TeamId,
                (t, project) =>
                {
                    project.Team = t;
                    return project;
                })
                .GroupJoin(
                taskList,
                p => p.Id,
                t => t.ProjectId,
                (project, t) =>
                {
                    project.Tasks = t.ToList();
                    return project;
                })
                .Select(
                project => new AboutProject()
                {
                    Project = project,
                    TheLongestTask = project
                    .Tasks?
                    .OrderByDescending(t => t.Description.Length)
                    .FirstOrDefault(),
                    TheShortestTask = project
                    .Tasks?
                    .OrderBy(t => t.Name.Length)
                    .FirstOrDefault(),
                    CountPlayers = project.Tasks.Count < 3
                    || project.Description.Length > 20
                    ? project.Team.UserList?.Count : 0
                }).OrderBy(p => p.Project.Id).ToList();

            return  _mapper.Map<List<AboutProjectDTO>>(aboutProject);
        }
        public void Dispose()
        {
        }
    }
}
