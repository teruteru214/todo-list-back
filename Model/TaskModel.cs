namespace Tasks.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string? Schedule { get; set; }
        public required bool Complete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
