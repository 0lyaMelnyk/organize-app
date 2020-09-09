using System;
using System.Collections.Generic;
using System.Text;
namespace HW3.DAL.Models
{
    public class TeamPlayers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> ListUser { get; set; }
    }
}
