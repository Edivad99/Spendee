using Spendee.API.Managers;
using Spendee.Database;
using Spendee.Database.Repository;

namespace Spendee.API.Extensions;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureCustomServices(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("MySQL");
        builder.Services.AddScoped<IWalletRepository>(_ => new WalletRepository(connectionString));
        builder.Services.AddScoped<ICategoryRepository>(_ => new CategoryRepository(connectionString));
        builder.Services.AddScoped<CategoriesManager>();
        builder.Services.AddScoped<WalletsManager>();
        
        return builder.Services;
    }
}
