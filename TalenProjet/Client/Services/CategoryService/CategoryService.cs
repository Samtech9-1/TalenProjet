using TalenProjet.Shared;

namespace TalenProjet.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }
        public List<Category> Categories { get ; set ; } = new List<Category>();
        public List<Category> AdminCategories { get; set; } = new List<Category>();

        public event Action OnChange;

        public async Task AddCategories(Category category)
        {
            var response = await _http.PostAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public Category CreateNewCategory()
        {
            var c = new Category { Isnew = true, Editing = true };
            AdminCategories.Add(c);
            OnChange.Invoke();
            return c;
        }

        public async Task DeleteCategories(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/Category/admin/{categoryId}");
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }

        public async Task GetAdminCategories()
        {
            var ressponse = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("/api/Category/admin");
            if (ressponse != null && ressponse.Data != null)
            {
                Categories = ressponse.Data;
            }
        }

        public async Task GetCategories()
        {
            var ressponse = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("/api/Category");
            if (ressponse != null && ressponse.Data != null) 
            {
                Categories = ressponse.Data;
            }

        }

        public async Task UpdateCategories(Category category)
        {
            var response = await _http.PutAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategories();
            OnChange.Invoke();
        }
    }
}
