namespace Spendee.Shared.Models;

public record TransactionDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public DateTime Date { get; set; }
    public CategoryDTO Category { get; set; }
}
