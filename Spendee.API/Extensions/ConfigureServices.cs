using Spendee.API.Managers;
using Spendee.Database;
using Spendee.Database.Repository;

namespace Spendee.API.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureCustomServices(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("MySQL");
        builder.Services.AddSingleton<IWalletRepository>(_ => new WalletRepository(connectionString));
        builder.Services.AddSingleton<ICategoryRepository>(_ => new CategoryRepository(connectionString));
        builder.Services.AddSingleton<CategoriesManager>();
        builder.Services.AddSingleton<WalletsManager>();
        
        return builder.Services;
    }
}
