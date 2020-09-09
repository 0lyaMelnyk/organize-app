using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.Common.DTO
{
    public class AboutProjectDTO
    {
        public ProjectDTO Project { get; set; }
        public TaskDTO TheLongestTask { get; set; }
        public TaskDTO TheShortestTask { get; set; }
        public int? CountPlayers { get; set; }
    }
}
