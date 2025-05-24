using Microsoft.EntityFrameworkCore;
using TeamTaskManagement.Core.Entities;
using static TeamTaskManagement.Infrastructure.EntitiesSeedData;

namespace TeamTaskManagement.Infrastructure
{
    public partial class TeamTaskServicesDBContext : DbContext
    {

        public TeamTaskServicesDBContext(DbContextOptions<TeamTaskServicesDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LookupSeedData());
            modelBuilder.ApplyConfiguration(new LookupItemsSeedData());

            modelBuilder.Entity<Lookups>()
      .HasMany(l => l.LookupItems)
      .WithOne(li => li.Lookups)
      .HasForeignKey(li => li.LookupId)
      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Priority)
                .WithMany(li => li.Priority)
                .HasForeignKey("PriorityId")
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Status)
                .WithMany(li => li.Status)
                .HasForeignKey("StatusId")
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Lookups> Lookups { get; set; }
        public DbSet<LookupItems> LookupItems { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

    }
}
