using HW3.BLL.ServicesAbstraction;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HW3.DAL.Repositories;
using HW3.Common.DTO;
using System.Linq;
using HW3.DAL.Abstracts;
using HW3.Common.Exeptions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HW3.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TaskModel> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TaskModel>();
        }

        public async Task CreateTask(TaskDTO task)
        {
            if (task == null) throw new Exception("You can`t create empty task");
            else await _repository.Create(_mapper.Map<TaskModel>(task)).ConfigureAwait(false);
        }

        public async Task DeleteTask(int id)
        {
            if (await _repository.Get(id).ConfigureAwait(false) == null) throw new NotFoundException($"Not found task with id={id}");
            else  await _repository.Delete(id).ConfigureAwait(false);
        }

        public async Task DeleteTask(TaskDTO task)
        {
            if (await _repository.Get(task.Id).ConfigureAwait(false) == null) throw new NotFoundException($"Not found task with id={task.Id}");
            else await _repository.Delete(_mapper.Map<TaskModel>(task).Id).ConfigureAwait(false);
        }

        public async Task UpdateTask(TaskDTO task)
        {
            var taskEntity = await _repository.Get(task.Id).ConfigureAwait(false);
            if (taskEntity == null)
            {
                throw new Exception($"Not found task with id={task.Id}");
            }
            else
            {
                taskEntity = _mapper.Map(task, taskEntity);
                await _repository.Update(taskEntity).ConfigureAwait(false);
            }
        }

        public async Task<TaskDTO> GetTaskById(int id)
        {
            var task = await _repository.Get(id).ConfigureAwait(false);
            if ( task== null) throw new Exception($"Not found task with id={id}");
            else return _mapper.Map<TaskDTO>(task);
        }

        public async Task<List<TaskDTO>> GetTasks()
        {
            return _mapper.Map<List<TaskDTO>>(await  _repository.Get().ConfigureAwait(false));
        }
        //Task 3
        public async Task<Dictionary<int, string>> GetListFinishedTasksAt2020(int performerId)
        {
            var tasks = await _repository.Get(x => x.PerformerId == performerId).ConfigureAwait(false);
            if (tasks.Count == 0)
                throw new NotFoundException("Task list for this user is empty");
            var quar = await _repository.Get(task => task.PerformerId == performerId && task.FinishedAt.Year == 2020
                 && task.State == TaskStateModel.Finished).ConfigureAwait(false);
            return await Task.FromResult(quar.ToDictionary(task => task.Id, task => task.Name)).ConfigureAwait(false);
        }
        //Task 2
        public async Task<List<TaskDTO>> GetTasksForUser(int performerId)
        {
            var tasks = await _repository.Get(task => task.PerformerId == performerId && task.Name.Length < 45).ConfigureAwait(false);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }
        public void Dispose()
        {
        }
    }
}
