using System.ComponentModel.DataAnnotations.Schema;

namespace GameStage.Models;

[Table("Payments")]
public class Payment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? StripePaymentId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    [ForeignKey("UserId")]
    public User User { get; set; } = default!;
}
