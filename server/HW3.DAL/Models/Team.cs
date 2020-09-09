using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW3.DAL.Models
{
    public class Team:TEntity
    {
        public override int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public List<User> UserList { get; set; }
        [NotMapped]
        public List<Project> ProjectList { get; set; }
    }
}
