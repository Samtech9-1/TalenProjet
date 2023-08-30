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
    }
}
