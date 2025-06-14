using System.ComponentModel.DataAnnotations.Schema;
using GameStage.Models;

[Table("ProjectFollowers")]
public class ProjectFollower
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    public Project Project { get; set; } = default!;
    public User User { get; set; } = default!;
}
