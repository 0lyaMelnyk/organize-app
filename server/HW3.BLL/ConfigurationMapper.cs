using AutoMapper;
using HW3.Common.DTO;
using HW3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.BLL
{
    public class ConfigurationMapper:Profile
    {
        public ConfigurationMapper()
        {
            CreateMap<TaskModel, TaskDTO>();
            CreateMap<TaskDTO, TaskModel>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();

            CreateMap<AboutProject, AboutProjectDTO>();
            CreateMap<AboutProjectDTO, AboutProject>();


            CreateMap<AboutLastProject, AboutLastProjectDTO>();
            CreateMap<AboutLastProjectDTO, AboutLastProject>();
        }
    }
}
