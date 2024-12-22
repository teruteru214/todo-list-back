using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name must be 100 characters or less.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(10, ErrorMessage = "Schedule must be 10 characters or less.")]
        public string? Schedule { get; set; }

        public bool Complete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
