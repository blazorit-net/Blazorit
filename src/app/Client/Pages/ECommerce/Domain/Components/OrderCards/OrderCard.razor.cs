using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.OrderCards
{
    public partial class OrderCard
    {
        [Parameter]
        public Order OrderData { get; set; } = new();

        [Parameter]
        public string? Class { get; set; }
    }
}
