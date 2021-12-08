using Spendee.API.Extensions;
using Spendee.API.Utils;
using Spendee.Database;
using Spendee.Database.Entity;
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

    public async Task<Response<IEnumerable<WalletDTO>>> GetAllWalletsAsync()
    {
        _logger.LogInformation("New requests GetAllWallets");
        try
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();
            var response = wallets
                .Select(wallet => wallet.ToDTO())
                .OrderByDescending(wallet => wallet.LastModified); ;
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

    public async Task<int> AddWallet(WalletDTO wallet)
    {
        _logger.LogInformation($"New requests AddNewWallet with wallet: {wallet}");
        try
        {
            var newWallet = new Wallet
            {
                Name = wallet.Name,
                Currency = wallet.Currency,
                LastModified = DateTime.Now
            };
            await _walletRepository.AddWallet(newWallet);

            _logger.LogInformation($"Added new wallet");

            return StatusCodes.Status201Created;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception in AddNewWallet with wallet: {wallet}");
            return StatusCodes.Status500InternalServerError;
        }
    }

    public async Task<Response<IEnumerable<TransactionDTO>>> GetTransactionsByWalletIdAsync(int walletID)
    {
        _logger.LogInformation($"New requests GetTransactionsByWalletIdAsync with id: {walletID}");
        try
        {
            var transactions = await _walletRepository.GetTransactionsByWalletIdAsync(walletID);
            var response = transactions
                .Select(transaction => transaction.ToDTO())
                .OrderByDescending(t => t.Date);
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

    public async Task<int> AddTransactionAsync(int walletID, TransactionDTO transaction)
    {
        _logger.LogInformation($"New requests AddTransactionAsync in walletId: {walletID} and transaction: {transaction}");
        try
        {
            var entityTransaction = new Transaction
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Date = transaction.Date,
                Price = transaction.Price,
                Category = new Category
                {
                    Id = transaction.Category.Id,
                    Name = transaction.Category.Name
                }
            };


            await _walletRepository.AddTransaction(walletID, entityTransaction);
            
            _logger.LogInformation($"Added new transaction");

            return StatusCodes.Status201Created;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception in AddTransactionAsync in walletId: {walletID} and transaction: {transaction}");
            return StatusCodes.Status500InternalServerError;
        }
    }
}
