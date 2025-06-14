using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStage.Models;

[Table("Devlogs")]
public class Devlog
{

    [Key]
    public int Id { get; set; }
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    [Required(ErrorMessage ="Obrigatorio informar o titulo do projeto")]
    public string Title { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(500)")]
    [Required(ErrorMessage = "Obrigatorio informar o conteudo do projeto")]
    public string? Content { get; set; }
    public string? MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = default!;
}
