namespace Spendee.Database.Entity;

public record Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public DateTime Date { get; set; }
    public Category Category { get; set; }
}
