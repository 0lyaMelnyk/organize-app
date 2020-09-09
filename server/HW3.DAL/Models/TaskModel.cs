using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HW3.DAL.Models
{
    public class TaskModel:TEntity
    {
        public override int Id { get; set; }
        [MinLength(2)]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public TaskStateModel State { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int? PerformerId { get; set; }
        public User Performer { get; set; }
    }
}
