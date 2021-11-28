using Spendee.Database;
using Spendee.Shared.Models;

namespace Spendee.API.Managers;

public class CategoriesManager
{
    private readonly ILogger<CategoriesManager> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesManager(ICategoryRepository categoryRepository, ILogger<CategoriesManager> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        _logger.LogInformation("New requests GetAllCategories");
        try
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var response = categories.Select(category => new CategoryDTO { Name = category.Name });
            _logger.LogInformation($"Request completed with {response.Count()} categories");
            return response;
        } catch(Exception e)
        {
            _logger.LogError(e, "Exception in GetAllCategories");
            return null;
        }
    }
}
