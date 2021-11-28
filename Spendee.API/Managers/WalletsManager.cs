using Spendee.API.Utils;
using Spendee.Database;
using Spendee.Shared.Models;

namespace Spendee.API.Managers;

public class WalletsManager
{
    private readonly ILogger<WalletsManager> _logger;
    private readonly IWalletRepository _walletRepository;

    public WalletsManager(IWalletRepository walletRepository, ILogger<WalletsManager> logger)
    {
        _logger = logger;
        _walletRepository = walletRepository;
    }

    public async Task<ResponseResult<IEnumerable<WalletDTO>>> GetAllWalletsAsync()
    {
        _logger.LogInformation("New requests GetAllWallets");
        try
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();
            var response = wallets.Select(wallet => new WalletDTO { Name = wallet.Name });
            _logger.LogInformation($"GetAllWallets's request completed with {response.Count()} wallets");
            return new()
            {
                Result = response,
                StatusCode = StatusCodes.Status200OK
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception in GetAllWallets");
            return new()
            {
                Result = null,
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    public async Task<ResponseResult<IEnumerable<TransactionDTO>>> GetTransactionsByWalletIdAsync(int walletID)
    {
        _logger.LogInformation($"New requests GetTransactionsByWalletIdAsync with id: {walletID}");
        try
        {
            var transactions = await _walletRepository.GetTransactionsByWalletIdAsync(walletID);
            var response = transactions
                .Select(transaction => new TransactionDTO 
                { 
                    Id = transaction.Id,
                    Price = transaction.Price,
                    Description = transaction.Description,
                    Date = transaction.Date,
                    Category = new CategoryDTO { Name = transaction.Category.Name }
                });
            _logger.LogInformation($"GetTransactionsByWalletIdAsync's request completed with {response.Count()} transactions");
            
            return new()
            {
                Result = response.Any() ? response : null,
                StatusCode = response.Any() ? StatusCodes.Status200OK : StatusCodes.Status404NotFound
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception in GetTransactionsByWalletIdAsync with id: {walletID}");
            return new()
            {
                Result = null,
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
