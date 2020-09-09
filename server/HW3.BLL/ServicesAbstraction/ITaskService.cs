using HW3.Common.DTO;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW3.BLL.ServicesAbstraction
{
    public interface ITaskService:IDisposable
    {
        Task DeleteTask(int id);
        Task DeleteTask(TaskDTO task);
        Task UpdateTask(TaskDTO task);
        Task CreateTask(TaskDTO task);
        Task<List<TaskDTO>> GetTasks();
        Task<TaskDTO> GetTaskById(int id);
        Task<Dictionary<int, string>> GetListFinishedTasksAt2020(int performerId);
        Task<List<TaskDTO>> GetTasksForUser(int performerId);
    }
}
