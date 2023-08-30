namespace TalenProjet.Client.Pages
{
    public partial class ProductDetails
    {
        private Product? product = null;
        private string message = string.Empty;
        public int currentTypeId = 1;

        [Parameter]
        public int Id { get; set; }

        protected override async  Task OnParametersSetAsync()
        {
            message = "loading product !";
            var res = await ProductService.GetProduct(Id);
            if (!res.Success)
            {
                message = res.Message;
            }
            else
            {
                product = res.Data;
                if (product.Variants.Count > 0) 
                {
                    currentTypeId = product.Variants[0].ProductTypeId;
                }
            }
        }
        private ProductVariant GetSelectedVAriant()
        {
            var variant = product.Variants.FirstOrDefault(v => v.ProductTypeId == currentTypeId);
            return variant;
        }
    }
}
