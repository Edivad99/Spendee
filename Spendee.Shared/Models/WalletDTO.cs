namespace Spendee.Shared.Models;

public record WalletDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime LastModified { get; set; }
    public char Currency { get; set; }
}
