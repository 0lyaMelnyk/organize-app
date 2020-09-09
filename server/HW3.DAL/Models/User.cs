using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HW3.DAL.Models
{
    public class User:TEntity
    {
        public override int Id { get; set; }
        [Required]
        [MaxLength(15)]
        [MinLength(2)]
        public string FirstName { get; set;}
        [Required]
        [MaxLength(15)]
        [MinLength(2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int? TeamId { get; set; }
        [NotMapped]
        public List<TaskModel> Tasks { get; set; }
    }
}
