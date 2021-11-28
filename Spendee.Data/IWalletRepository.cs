using Spendee.Database.Entity;

namespace Spendee.Database;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetAllWalletsAsync();
    Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(int walletID);
}
