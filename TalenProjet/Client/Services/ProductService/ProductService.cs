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

        public async Task GetProducts(string categoryUrl)
        { 
            var res = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");
            if (res != null && res.Data != null)
            {
                Products = res.Data;
            }
            ProductChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productid}");
            return result;
        }

        public async Task SearchProduct(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");
            if (result != null && result.Data != null)
            {
                Products = result.Data;
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
