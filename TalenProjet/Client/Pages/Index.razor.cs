namespace TalenProjet.Client.Pages
{
    public partial class Index
    {
        [Parameter]
        public string? CategoryUrl { get; set; } = null;
        [Parameter]
        public string? SearchText { get; set; } = null;

        [Parameter]
        public int Page { get; set; } = 1;

        protected override async Task OnParametersSetAsync()
        {
            if (SearchText != null) 
            { 
                await ProductService.SearchProduct(SearchText, Page);
            }
            else
            {
                await ProductService.GetProducts(CategoryUrl);
            } 
        }


    }
}
