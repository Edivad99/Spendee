using Dapper;
using Dapper.Transaction;
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

    public async Task AddTransaction(int walletID, Transaction transaction)
    {
        var sql = @"INSERT INTO `Transactions` (`WalletID`, `CategoryID`, `Description`, `Price`, `Date`) VALUES
                    (@WALLETID, @CATEGORYID, @DESCRIPTION, @PRICE, @DATE);";

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@WALLETID", walletID, DbType.Int32, ParameterDirection.Input);
        dynamicParameters.Add("@CATEGORYID", transaction.Category.Id, DbType.Int32, ParameterDirection.Input);
        dynamicParameters.Add("@DESCRIPTION", transaction.Description, DbType.String, ParameterDirection.Input);
        dynamicParameters.Add("@PRICE", transaction.Price, DbType.Decimal, ParameterDirection.Input);
        dynamicParameters.Add("@DATE", transaction.Date, DbType.DateTime, ParameterDirection.Input);

        var sql2 = @"UPDATE `Wallets` SET `LastModified` = @DATE WHERE `Id` = @WALLETID;";
        var dynamicParameters2 = new DynamicParameters();
        dynamicParameters2.Add("@DATE", transaction.Date, DbType.DateTime, ParameterDirection.Input);
        dynamicParameters2.Add("@WALLETID", walletID, DbType.Int32, ParameterDirection.Input);
        

        using var conn = GetDbConnection();
        conn.Open();
        using var sqlTransaction = conn.BeginTransaction();
        try
        {
            await sqlTransaction.ExecuteAsync(sql, dynamicParameters);
            await sqlTransaction.ExecuteAsync(sql2, dynamicParameters2);
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            try
            {
                sqlTransaction.Rollback();
            }
            catch (Exception ex2)
            {
                throw new Exception(ex2.Message, ex2.InnerException);
            }
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
