using System;
using System.Collections.Generic;
using System.Text;
namespace HW3.Common.DTO
{
    public class TeamPlayersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserDTO> ListUser { get; set; }
    }
}
