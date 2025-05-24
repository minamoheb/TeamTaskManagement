using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamTaskManagement.Core.Entities;

namespace TeamTaskManagement.Infrastructure
{
    public static class EntitiesSeedData
    {
        public class LookupSeedData : IEntityTypeConfiguration<Lookups>
        {
            public void Configure(EntityTypeBuilder<Lookups> builder)
            {
                builder.ToTable("Lookups");
                builder.HasData(
                 new Lookups { Id = 1, Name = "Status", NameAR = "الحالة", CreatedBy = 1 },
                 new Lookups { Id = 2, Name = "Priority", NameAR = "الأولوية", CreatedBy = 1 }
                );
            }
        }
        public class LookupItemsSeedData : IEntityTypeConfiguration<LookupItems>
        {
            public void Configure(EntityTypeBuilder<LookupItems> builder)
            {
                builder.ToTable("LookupItems");
                builder.HasData(
                new LookupItems { Id = 1, LookupId = 1, NameEn = "Open", NameAR = "مفتوح", CreatedBy = 1 },
                new LookupItems { Id = 2, LookupId = 1, NameEn = "In Progress", NameAR = "قيد التنفيذ", CreatedBy = 1 },
                new LookupItems { Id = 3, LookupId = 1, NameEn = "Closed", NameAR = "مغلق", CreatedBy = 1 },
                new LookupItems { Id = 4, LookupId = 2, NameEn = "Low", NameAR = "منخفض", CreatedBy = 1 },
                new LookupItems { Id = 5, LookupId = 2, NameEn = "Medium", NameAR = "متوسط", CreatedBy = 1 },
                new LookupItems { Id = 6, LookupId = 2, NameEn = "High", NameAR = "مرتفع", CreatedBy = 1 }
                );
            }
        }

    }

}