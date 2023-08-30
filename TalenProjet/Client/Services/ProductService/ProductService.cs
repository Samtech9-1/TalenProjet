namespace TalenProjet.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task GetProducts()
        {
            var res = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if (res != null && res.Data != null)
            {
                Products = res.Data;
            }
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productid)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productid}");
            return result;
        }
    }
}
