using Spendee.Database.Entity;

namespace Spendee.Database;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetAllWalletsAsync();
    Task AddWallet(Wallet wallet);
    Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(int walletID);
    Task AddTransaction(int walletID, Transaction transaction);
}
