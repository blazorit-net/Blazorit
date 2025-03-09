using Orders = Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Microsoft.AspNetCore.Components;
using Orders_OrderItem = Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders.OrderItem;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.OrderCards.Comps.OrderItems
{
    public partial class OrderItem
    {
        bool isImagePreviewVisible = false;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public Orders_OrderItem Item { get; set; } = new();
    }
}
