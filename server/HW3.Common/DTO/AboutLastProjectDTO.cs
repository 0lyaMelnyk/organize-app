using System;
using System.Collections.Generic;
using System.Text;
namespace HW3.Common.DTO
{
    public class AboutLastProjectDTO
    {
        public UserDTO User { get; set; }
        public ProjectDTO LastProject { get; set; }
        public int CountTasks { get; set; }
        public int CountNotFinishedOrCanceledTasks { get; set; }
        public TaskDTO LongestTask { get; set; }
    }
}
