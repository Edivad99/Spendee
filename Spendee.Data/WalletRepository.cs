using Dapper;
using MySql.Data.MySqlClient;
using Spendee.Database.Entity;
using System.Data;

namespace Spendee.Database;

public class WalletRepository
{
    private readonly string ConnectionString;

    public WalletRepository(string connectionString) => ConnectionString = connectionString;

    private IDbConnection GetDbConnection() => new MySqlConnection(ConnectionString);

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int walletID)
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
