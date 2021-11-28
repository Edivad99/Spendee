using Spendee.Database.Entity;

namespace Spendee.Database;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
}
