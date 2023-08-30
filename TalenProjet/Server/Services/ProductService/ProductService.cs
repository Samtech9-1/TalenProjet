using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata.Ecma335;
using TalenProjet.Server.Data;
using TalenProjet.Shared;

namespace TalenProjet.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _contect;

        public ProductService(DataContext context)
        {
            _contect = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsListAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _contect.Products.ToListAsync(),
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var resposne = new ServiceResponse<Product>();
            var product = await _contect.Products.FindAsync(productId);
            if (product == null)
            {
                resposne.Message = "sorry, the prouct is not found !";
                resposne.Success = false;
            }
            else
            {
                resposne.Data = product;
            }
            return resposne;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _contect.Products
                       .Where(x => x.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                       .ToListAsync()
            };
            return response;
        }
    }
}
