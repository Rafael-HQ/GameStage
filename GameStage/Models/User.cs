using System.ComponentModel.DataAnnotations.Schema;
using GameStage.Models;

[Table("Users")]
public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectFollower> FollowedProjects { get; set; } = new List<ProjectFollower>();
    public ICollection<LiveMessage> Messages { get; set; } = new List<LiveMessage>();
    public ICollection<LiveStream> LiveStreams { get; set; } = new List<LiveStream>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public enum UserRole
{
    User = 0,
    Developer = 1
}
