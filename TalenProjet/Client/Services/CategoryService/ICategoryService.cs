using TalenProjet.Shared;

namespace TalenProjet.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChange; 

        List<Category> Categories { get; set; }

        List<Category> AdminCategories { get; set; }

        Task GetCategories();

        Task GetAdminCategories();

        Task AddCategories(Category category);

        Task DeleteCategories(int categoryId);

        Task UpdateCategories(Category category);

        Category CreateNewCategory();
    }
}
