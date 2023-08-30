namespace TalenProjet.Client.Pages
{
    public partial class ProductDetails
    {
        private Product? product = null;
        private string message = string.Empty;

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
            }
        }
    }
}
