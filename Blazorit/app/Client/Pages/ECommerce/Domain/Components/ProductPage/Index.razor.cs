using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Microsoft.AspNetCore.Components;


namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage
{
    public partial class Index {
        private ProductCardData productCard = new();

        public Index() { }

        [Parameter] public string Category { get; set; } = "empty";
        [Parameter] public string LinkPart { get; set; } = "empty";


        protected override async Task OnParametersSetAsync() {
            productCard = await DataService.GetProductDataAsync(Category, LinkPart);
        }
    }
}
