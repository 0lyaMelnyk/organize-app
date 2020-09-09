using HW3.Common.DTO;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HW3.BLL.ServicesAbstraction
{
    public interface IProjectService:IDisposable
    {
        Task DeleteProject(int id);
        Task DeleteProject(ProjectDTO project);
        Task UpdateProject(ProjectDTO project);
        Task CreateProject(ProjectDTO project);
        Task<List<ProjectDTO>> GetProjects();
        Task<ProjectDTO> GetProjectById(int id);
        Task<List<AboutProjectDTO>> GetInfoAboutProjects();
        Task<Dictionary<int, int>> GetCountTasksByUser(int authorId);
    }
}
