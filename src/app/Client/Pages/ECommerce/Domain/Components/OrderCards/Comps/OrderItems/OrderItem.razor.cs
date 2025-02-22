using Orders = Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.OrderCards.Comps.OrderItems
{
    public partial class OrderItem
    {
        bool isImagePreviewVisible = false;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public Orders.OrderItem Item { get; set; } = new();
    }
}
