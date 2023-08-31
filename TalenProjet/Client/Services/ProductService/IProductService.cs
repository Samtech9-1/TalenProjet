namespace TalenProjet.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        List<Product> Products { get; set; }
        string Message { get; set; } 
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProduct(int productid);
        Task SearchProduct(string searchText, int page);
        Task<List<string>> GetProductsearchSuggestion(string searchText);

    }
}
