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
                Data = await _contect.Products.Include(p => p.Variants).ToListAsync(),
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
            var resposne = new ServiceResponse<Product>();
            var product = await _contect.Products
                                        .Include(p => p.Variants)
                                        .ThenInclude(v => v.ProductType)
                                        .FirstOrDefaultAsync(p => p.Id == productId);
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
                        .Include(p => p.Variants)
                       .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await FindPrductsBySearctet(searchText)

            };
            return response;
        }

        private async Task<List<Product>> FindPrductsBySearctet(string searchText)
        {
            return await _contect.Products
                                 .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                                 ||
                                 p.Description.ToLower().Contains(searchText.ToLower()))
                                 .Include(p => p.Variants)
                                 .ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetProductsSearchSuggestions(string searchText)
        {
            var products = await FindPrductsBySearctet(searchText);
            List<string> result = new List<string>();
            foreach (var product in products) 
            { 
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }
                if (product.Description != null)
                {
                    var puntuation = product.Description.Where(char.IsPunctuation)
                                     .Distinct().ToArray();
                    var words = product.Description.Split()
                                .Select(s => s.Trim(puntuation));
                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }
            return new ServiceResponse<List<string>> { Data = result };

        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _contect.Products.Where(p => p.Featured)
                                              .Include(p => p.Variants)
                                              .ToListAsync(),
            };
            return response;
        }
    }
}
