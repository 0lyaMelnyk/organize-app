using System;
using System.Collections.Generic;
using System.Text;

namespace HW3.Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int? TeamId { get; set; }
        public List<TaskDTO> Tasks { get; set; }
    }
}
