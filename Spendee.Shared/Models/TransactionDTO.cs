using Spendee.Shared.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Spendee.Shared.Models;

public record TransactionDTO
{
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    [NotZero]
    public float Price { get; set; }
    public DateTime Date { get; set; }
    public CategoryDTO Category { get; set; }
}
