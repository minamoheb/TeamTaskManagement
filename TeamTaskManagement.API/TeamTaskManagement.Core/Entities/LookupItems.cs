using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamTaskManagement.Core.Entities
{
    public partial class LookupItems : BaseEntity
    {
        [Required]
        public long LookupId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Code { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? NameEn { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? NameAR { get; set; }
        [ForeignKey("LookupId")]
        public virtual Lookups Lookups { get; set; }
        public virtual ICollection<Tasks> Priority { get; set; }
        public virtual ICollection<Tasks> Status { get; set; }

    }
}
