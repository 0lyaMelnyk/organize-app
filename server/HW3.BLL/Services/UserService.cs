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
    public class UserService:IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<TaskModel> _repositoryTask;
        private readonly IRepository<Project> _repositoryProject;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<User>();
            _repositoryTask = _unitOfWork.GetRepository<TaskModel>();
            _repositoryProject = _unitOfWork.GetRepository<Project>();
            _mapper = mapper;
        }

        public async Task DeleteUser(int id)
        {
            if (_repository.Get(id) == null)
            {
                throw new NotFoundException($"Not found user with id={id}");
            }
            else
            {
                await DeleteDataForUser(id).ConfigureAwait(false);
                await _repository.Delete(id).ConfigureAwait(false);
            }
        }

        public async Task DeleteUser(UserDTO user)
        {
            if (user == null)
            {
                throw new NotFoundException("You can`t delete empty user");
            }
            else
            {
                await DeleteDataForUser(user.Id).ConfigureAwait(false);
                await _repository.Delete(_mapper.Map<User>(user).Id).ConfigureAwait(false);
            }
        }

        public async Task DeleteDataForUser(int id)
        {
            var user = await _repository.Get(id).ConfigureAwait(false);
            if (user == null) throw new NotFoundException($"Not found user with {id}");
            var tasks = await _repositoryTask.Get(x => x.PerformerId == id).ConfigureAwait(false);
            for (int i = 0; i < tasks?.Count(); i++)
            {
                var task = await _repositoryTask.Get(tasks[i].Id).ConfigureAwait(false);
                task.PerformerId = null;
            }
            var projects = await _repositoryProject.Get(x => x.AuthorId == id).ConfigureAwait(false);
            for (int i = 0; i < projects?.Count(); i++)
            {
                await _repositoryProject.Delete(projects[i].Id).ConfigureAwait(false);
            }
        }
        public async Task UpdateUser(UserDTO user)
        {
            var userEntity = await _repository.Get(user.Id).ConfigureAwait(false);

            if (userEntity == null)
            {
                throw new Exception($"Not found user with id={user.Id}");
            }
            else
            {
                userEntity = _mapper.Map(user, userEntity);
                await _repository.Update(userEntity).ConfigureAwait(false);
            }
        }

        public async Task CreateUser(UserDTO user)
        {
            if (user == null)
                throw new Exception("Can`t create empty user");
            else await _repository.Create(_mapper.Map<User>(user)).ConfigureAwait(false);
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return _mapper.Map<List<UserDTO>>(await _repository.Get().ConfigureAwait(false));
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            return _mapper.Map<UserDTO>(await _repository.Get(id).ConfigureAwait(false));
        }
        //Task 5
        public async Task<List<UserDTO>> GetListUserByFirstName()
        {
            var users = await _repository.Get().ConfigureAwait(false);
            var tasks = await _repositoryTask.Get().ConfigureAwait(false);
            return users.GroupJoin(tasks,
                u => u.Id,
                t => t.PerformerId,
                (u, t) => {
                    u.Tasks = t.OrderByDescending(x => x.Name.Length).ToList();
                    return u;
                })
                .OrderBy(x => x.FirstName)
                .Select(u=>_mapper.Map<UserDTO>(u)).ToList();
        }
        //Task 6
        public async Task<AboutLastProjectDTO> GetInfoAboutLastProjectByUserId(int authorId)
        {
            User user = await _repository.Get(authorId).ConfigureAwait(false);
            List<Project> projectList = await _repositoryProject.Get().ConfigureAwait(false);
            List<TaskModel> taskList = await _repositoryTask.Get().ConfigureAwait(false);
            AboutLastProject project = projectList
                .GroupJoin(
                taskList,
                t => t.Id,
                p => p.ProjectId,
                (_, __) => new AboutLastProject()
                {
                    User = user,
                    LastProject = projectList
                    .Where(x => x.AuthorId == authorId)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault(),
                    CountTasks = taskList
                    .Count(task => task.ProjectId == projectList
                    .Where(x => x.AuthorId == authorId)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault()?.Id
                    ),
                    CountNotFinishedOrCanceledTasks = taskList
                    .Count(task => ((int)task.State != 2)
                    && task.PerformerId == authorId),
                    LongestTask = taskList
                    .Where(task => task.PerformerId == authorId)
                    .OrderByDescending(x => x.FinishedAt - x.CreatedAt)
                    .FirstOrDefault()
                })
                .FirstOrDefault();
            return _mapper.Map<AboutLastProjectDTO>(project);
        }
        //task 8
        public async Task<List<TaskDTO>> GetNotFinishedTasksForUser(int performerId)
        {
            var user =await _repository.Get(performerId).ConfigureAwait(false);
            if (user == null) throw new NotFoundException();
            List<TaskModel> tasks =await _repositoryTask.Get(t => t.PerformerId == performerId && t.State != TaskStateModel.Finished).ConfigureAwait(false);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }
        public void Dispose()
        {
        }
    }
}
