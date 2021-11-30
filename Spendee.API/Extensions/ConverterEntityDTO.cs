using Spendee.Database.Entity;
using Spendee.Shared.Models;

namespace Spendee.API.Extensions;

public static class ConverterEntityDTO
{
    public static CategoryDTO ToDTO(this Category category)
    {
        return new()
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static WalletDTO ToDTO(this Wallet wallet)
    {
        return new()
        {
            Id = wallet.Id,
            Name = wallet.Name,
            LastModified = wallet.LastModified,
            Currency = wallet.Currency
        };
    }

    public static TransactionDTO ToDTO(this Transaction transaction)
    {
        return new()
        {
            Id = transaction.Id,
            Price = transaction.Price,
            Description = transaction.Description,
            Date = transaction.Date,
            Category = transaction.Category.ToDTO()
        };
    }
}
