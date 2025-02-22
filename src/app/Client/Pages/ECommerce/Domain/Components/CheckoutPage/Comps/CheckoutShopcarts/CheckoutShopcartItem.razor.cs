using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.CheckoutShopcarts
{
    public partial class CheckoutShopcartItem
    {
        bool isImagePreviewVisible = false;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public CartItem Item { get; set; } = null!;
    }
}
