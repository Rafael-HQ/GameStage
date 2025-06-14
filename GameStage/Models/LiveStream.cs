using System.ComponentModel.DataAnnotations.Schema;
using GameStage.Models;

[Table("LiveStreams")]
public class LiveStream
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string StreamKey { get; set; } = string.Empty;
    public string? MuxStreamId { get; set; }
    public bool IsLive { get; set; } = false;
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    [ForeignKey("ProjectId")]
    public Project Project { get; set; } = default!;
    [InverseProperty("LiveStream")]
    public ICollection<LiveMessage> Messages { get; set; } = new List<LiveMessage>();
}

