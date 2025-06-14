using System.ComponentModel.DataAnnotations.Schema;

namespace GameStage.Models;

[Table("LiveMessages")]
public class LiveMessage
{
    public int Id { get; set; }
    public int LiveStreamId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("LiveStreamId")]
    public LiveStream LiveStream { get; set; } = default!;
    [ForeignKey("UserId")]
    public User User { get; set; } = default!;
}
