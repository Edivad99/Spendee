namespace Spendee.Database.Entity;

public record Wallet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime LastModified { get; set; }
    public string Currency { get; set; }
}
