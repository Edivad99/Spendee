using Dapper;
using Spendee.Database.Entity;
using System.Data;

namespace Spendee.Database.Repository;

public class WalletRepository : Repository, IWalletRepository
{
    public WalletRepository(string connectionString) : base(connectionString) { }

    public async Task<IEnumerable<Wallet>> GetAllWalletsAsync() => await GetAllEntries<Wallet>("SELECT * FROM Wallets;");

    public async Task<IEnumerable<Transaction>> GetTransactionsByWalletIdAsync(int walletID)
    {
        var sql = @"SELECT * 
                    FROM Transactions t, Categories c 
                    WHERE t.CategoryID = c.ID AND WalletID = @WALLETID;";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@WALLETID", walletID, DbType.Int32, ParameterDirection.Input);

        using var conn = GetDbConnection();
        var result = await conn.QueryAsync<Transaction, Category, Transaction>(sql, (transaction, category) =>
        {
            transaction.Category = category;
            return transaction;
        }, dynamicParameters);
        return result;
    }
}
