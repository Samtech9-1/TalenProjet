﻿namespace TalenProjet.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsListAsync();
        Task<ServiceResponse<Product>> GetProductAsync( int productId);
        Task<ServiceResponse<List<Product>>> GetProductByCategory( string categoryUrl);
        Task<ServiceResponse<ProductSearcResultDTO>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProductsSearchSuggestions(string searchText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    }
}
