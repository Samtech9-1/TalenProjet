namespace TalenProjet.Client.Pages
{
    public partial class Index
    {
        [Parameter]
        public string? CategoryUrl { get; set; } = null;
        [Parameter]
        public string? SearchText { get; set; } = null;

        protected override async Task OnParametersSetAsync()
        {
            if (SearchText != null) 
            { 
                await ProductService.SearchProduct(SearchText);
            }
            else
            {
                await ProductService.GetProducts(CategoryUrl);
            } 
        }


    }
}
