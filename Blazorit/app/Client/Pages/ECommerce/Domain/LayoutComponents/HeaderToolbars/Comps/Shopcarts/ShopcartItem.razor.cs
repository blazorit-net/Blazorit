using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.Shopcarts
{
    public partial class ShopcartItem
    {
        bool isImagePreviewVisible = false;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public CartItem Item { get; set; } = null!;

        [Parameter]
        public bool IsVisibleShopcartDrawer { get; set; } = false;

        [Parameter]
        public EventCallback<bool> IsVisibleShopcartDrawerChanged { get; set; }


        private async Task ProductNameButton_ClickHandler(CartItem item)
        {
            ////NavigationManager.NavigateTo(PageRouter.RefToProductPage(item));
            await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
        }
    }
}
