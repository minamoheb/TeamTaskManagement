namespace TeamTaskManagement.Services.Dtos
{
    public class TaskModel
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public long? PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public long? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedTo { get; set; }
    }

}
