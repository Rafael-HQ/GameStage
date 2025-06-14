using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStage.Data
{
    public class ProjectFollowerTypeConfig : IEntityTypeConfiguration<ProjectFollower>
    {
        public void Configure(EntityTypeBuilder<ProjectFollower> builder)
        {
            builder.HasKey(pf => pf.Id);
            builder.Property(pf => pf.FollowedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(pf => pf.User)
                .WithMany(u => u.FollowedProjects)
                .HasForeignKey(pf => pf.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pf => pf.Project)
                .WithMany(p => p.Followers)
                .HasForeignKey(pf => pf.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
 }

