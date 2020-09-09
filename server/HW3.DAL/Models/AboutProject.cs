using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.DAL.Models
{
    public class AboutProject
    {
        public Project Project { get; set; }
        public TaskModel TheLongestTask { get; set; }
        public TaskModel TheShortestTask { get; set; }
        public int? CountPlayers { get; set; }
    }
}
