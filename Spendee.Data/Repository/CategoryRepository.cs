using Spendee.Database.Entity;

namespace Spendee.Database.Repository;

public class CategoryRepository : Repository
{
    public CategoryRepository(string connectionString) : base(connectionString) { }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync() => await GetAllEntries<Category>("SELECT * FROM Categories;");
}
