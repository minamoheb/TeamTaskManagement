using System.ComponentModel.DataAnnotations.Schema;

namespace TeamTaskManagement.Core.Entities
{
    public partial class Lookups : BaseEntity
    {
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string NameAR { get; set; }
        public virtual ICollection<LookupItems> LookupItems { get; set; }

    }
}
