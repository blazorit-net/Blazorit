using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage.Comps.ProductCards
{
    public partial class ProductCard
    {
        [Parameter] public string? Class { get; set; }

        [Parameter] public string Name { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
        }
    }
}
