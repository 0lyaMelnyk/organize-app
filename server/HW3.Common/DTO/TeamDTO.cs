using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.Common.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserDTO> UserList { get; set; }
    }
}
