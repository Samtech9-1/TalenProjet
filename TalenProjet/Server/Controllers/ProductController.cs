

using TalenProjet.Server.Data;
using TalenProjet.Shared;

namespace TalenProjet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {       
        private readonly IProductService _productService;

        public ProductController(IProductService productService )
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct()
        {
            var products = await _productService.GetProductsListAsync();
            var response = new ServiceResponse<List<Product>>
            {
                Data = products.Data
            };
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId )
        {
            var product = await _productService.GetProductAsync(productId);          
            return Ok(product);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByCategory(string categoryUrl)
        {
            var product = await _productService.GetProductByCategory(categoryUrl);
            return Ok(product);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetSearchProducts(string searchText)
        {
            var product = await _productService.SearchProducts(searchText);
            return Ok(product);
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductsSearchSuggestions(string searchText)
        {
            var product = await _productService.GetProductsSearchSuggestions(searchText);
            return Ok(product);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetFeaturedProducts()
        {
            var product = await _productService.GetFeaturedProducts();
            return Ok(product);
        }
    }
}
