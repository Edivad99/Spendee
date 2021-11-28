using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Spendee.Database.Repository;

public abstract class Repository
{
    private readonly string ConnectionString;

    public Repository(string connectionString) => ConnectionString = connectionString;

    protected IDbConnection GetDbConnection() => new MySqlConnection(ConnectionString);

    protected async Task<IEnumerable<T>> GetAllEntries<T>(string sql)
    {
        using var conn = GetDbConnection();
        var result = await conn.QueryAsync<T>(sql);
        return result;
    }
}
