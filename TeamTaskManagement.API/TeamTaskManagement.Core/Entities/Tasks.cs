using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamTaskManagement.Core.Entities
{
    public partial class Tasks : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public long PriorityId { get; set; }
        [Required]
        public long StatusId { get; set; }
        public DateTime? DueDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(200)")]
        public string? AssignedTo { get; set; } = string.Empty;

        [ForeignKey(nameof(PriorityId))]
        public virtual LookupItems? Priority { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual LookupItems? Status { get; set; }

    }
}
