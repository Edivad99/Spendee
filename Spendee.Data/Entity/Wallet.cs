namespace Spendee.Database.Entity;

public record Wallet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Transaction> Transactions { get; set;} //Probabilmente non ha senso, da valutare la rimozione
}
