

using System.Net.Http.Json;
using System.Xml;

namespace TalenProjet.Client.Shared
{
    public partial class ProductList
    {
        private static List<Product> Products = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
             await Productservice.GetProducts();
        }
    }
}
