using TalenProjet.Shared;

namespace TalenProjet.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public event Action ProductChanged;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();
        public string Message { get; set; } = "Chargement des produits...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public async Task GetProducts(string? categoryUrl)
        { 
            var res = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (res != null && res.Data != null)
            {
                Products = res.Data;
            }
            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
            {
                Message = "il n'y a pas de produit !";
            }

            ProductChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productid}");
            return result;
        }

        public async Task SearchProduct(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearcResultDTO>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
                    
            }
            if (Products.Count == 0) 
            {
                Message = "le produit n'exixte pas !";
            }
            ProductChanged.Invoke();
        }

        public async Task<List<string>> GetProductsearchSuggestion(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchsuggestions/{searchText}");
            return result.Data;
        }
    }
}
