using System.ComponentModel.DataAnnotations;

namespace Spendee.Shared.Models;

public record WalletDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime LastModified { get; set; }
    [Required]
    public string Currency { get; set; }
}
