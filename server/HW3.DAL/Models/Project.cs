using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW3.DAL.Models
{
    public class Project:TEntity
    {
        public override int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public int? AuthorId { get; set; }
        public int? TeamId { get; set; }
        [NotMapped]
        public List<TaskModel> Tasks { get; set; } 
        [NotMapped]
        public Team Team { get; set; }
        [NotMapped]
        public User Author { get; set; }
    }
}
