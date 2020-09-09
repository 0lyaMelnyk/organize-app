using HW3.Common.DTO;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW3.BLL.ServicesAbstraction
{
    public interface IUserService:IDisposable
    {
        Task DeleteUser(int id);
        Task DeleteUser(UserDTO user);
        Task UpdateUser(UserDTO user);
        Task CreateUser(UserDTO user);
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUserById(int id);
        Task<List<UserDTO>> GetListUserByFirstName();
        Task<AboutLastProjectDTO> GetInfoAboutLastProjectByUserId(int authorId);
        Task<List<TaskDTO>> GetNotFinishedTasksForUser(int performerId);

    }
}
