using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage {
    public partial class Index { 
        private string productName = string.Empty;

        public Index() { }

        [Parameter] public string Category { get; set; } = "empty";
        [Parameter] public string LinkPart { get; set; } = "empty";


        protected override async Task OnParametersSetAsync() {
            productName = await dataService.GetProductData(Category, LinkPart);

        }
    }
}
