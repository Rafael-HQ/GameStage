namespace GameStage.Data;

using System.Reflection;
using GameStage.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Devlog> Devlogs => Set<Devlog>();
    public DbSet<LiveStream> LiveStreams => Set<LiveStream>();
    public DbSet<LiveMessage> LiveMessages => Set<LiveMessage>();
    public DbSet<ProjectFollower> ProjectFollowers => Set<ProjectFollower>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectFollower>()
            .HasIndex(pf => new { pf.ProjectId, pf.UserId })
            .IsUnique();

        modelBuilder.Entity<ProjectFollower>()
            .HasOne(pf => pf.User)
            .WithMany(u => u.FollowedProjects)
            .HasForeignKey(pf => pf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectFollower>()
            .HasOne(pf => pf.Project)
            .WithMany(p => p.Followers)
            .HasForeignKey(pf => pf.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany(u => u.Payments)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LiveMessage>()
            .HasOne(m => m.User)
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LiveMessage>()
            .HasOne(m => m.LiveStream)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => m.LiveStreamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LiveStream>()
            .HasOne(s => s.Project)
            .WithMany(p => p.LiveStreams)
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
