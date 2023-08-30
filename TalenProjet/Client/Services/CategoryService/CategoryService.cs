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

        public async Task GetCategories()
        {
            var ressponse = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("/api/category");
            if (ressponse != null && ressponse.Data != null) 
            {
                Categories = ressponse.Data;
            }

        }
    }
}
