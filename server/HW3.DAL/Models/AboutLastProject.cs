using System;
using System.Collections.Generic;
using System.Text;
namespace HW3.DAL.Models
{
    public class AboutLastProject
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Project LastProject { get; set; }
        public int ProjectId { get; set; }
        public int CountTasks { get; set; }
        public int CountNotFinishedOrCanceledTasks { get; set; }
        public TaskModel LongestTask { get; set; }
        public int LongestTaskId { get; set; }
        public int? ShortestTaskId { get; set; }
        public TaskModel ShortestTask { get; set; }
    }
}
