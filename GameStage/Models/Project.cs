using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStage.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required(ErrorMessage ="Obrigatorio informar nome do projeto")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "Obrigatorio informar o conteudo do projeto")]
        public string Description { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = default!;
        public ICollection<Devlog> Devlogs { get; set; } = new List<Devlog>();
        public ICollection<LiveStream> LiveStreams { get; set; } = new List<LiveStream>();
        public ICollection<ProjectFollower> Followers { get; set; } = new List<ProjectFollower>();
    }

}
