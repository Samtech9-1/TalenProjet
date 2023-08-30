using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata.Ecma335;
using TalenProjet.Server.Data;

namespace TalenProjet.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext )
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsListAsync()
        {            
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _dataContext.Products.ToListAsync(),
            };
            return response;
        }
    }
}
