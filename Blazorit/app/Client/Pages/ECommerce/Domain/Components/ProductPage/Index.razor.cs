using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Microsoft.AspNetCore.Components;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Data;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage
{
    /// <summary>
    /// ProductPage
    /// </summary>
    public partial class Index {
        private ProductCardData productCard = new();

        public Index() { }

        [Inject] private IDataService DataService { get; set; } = null!;

        [Parameter] public string? Class { get; set; }

        [Parameter] public string Category { get; set; } = "empty";
        [Parameter] public string LinkPart { get; set; } = "empty";


        protected override async Task OnParametersSetAsync() {
            productCard = await DataService.GetProductDataAsync(Category, LinkPart);
        }
    }
}
