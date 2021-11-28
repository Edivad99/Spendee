using Spendee.Database;
using Spendee.Shared.Models;

namespace Spendee.API.Managers;

public class CategoriesManager
{
    private readonly ILogger<CategoriesManager> logger;
    private readonly ICategoryRepository categoryRepository;

    public CategoriesManager(ICategoryRepository categoryRepository, ILogger<CategoriesManager> logger)
    {
        this.categoryRepository = categoryRepository;
        this.logger = logger;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        logger.LogInformation("New requests GetAllCategories");
        try
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();
            var response = categories.Select(category => new Category { Name = category.Name });
            logger.LogInformation($"Request completed with {response.Count()} categories");
            return response;
        } catch(Exception e)
        {
            logger.LogError(e, "Exception in GetAllCategories");
            return null;
        }
    }
}
