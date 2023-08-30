﻿namespace TalenProjet.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsListAsync();
        Task<ServiceResponse<Product>> GetProductAsync( int productId);
    }
}
