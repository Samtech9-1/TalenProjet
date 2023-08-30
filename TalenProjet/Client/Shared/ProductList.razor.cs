namespace TalenProjet.Client.Shared
{
    public partial class ProductList
    {
        private static List<Product> Products = new List<Product>();

        protected override void OnInitialized()
        {
            Productservice.ProductChanged += StateHasChanged;
        }
        public void Dispose()
        {
            Productservice.ProductChanged -= StateHasChanged;
        }

        private string GetPriceText(Product product)
        {
            var variants = product.Variants;
            if (variants.Count == 0) 
            { 
                return string.Empty;
            }
            else if (variants.Count == 1) 
            {
                return $"{variants[0].Price} €";
            } 
            else
            {
                decimal minPrice =  variants.Min(x => x.Price);

                return $"A partir de {minPrice} €";

            }

        }
    }
}
