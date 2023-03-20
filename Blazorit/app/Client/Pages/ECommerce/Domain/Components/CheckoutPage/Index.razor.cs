using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage
{
    public partial class Index
    {
        private DeliveryMethod selectedDeliveryMethod = new();
        private DeliveryAddress selectedDeliveryAddress = new();
        private DeliveryCost deliveryCost = new();
        private SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries.UserDeliveryPoint delivery = new();

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Parameter] 
        public string? Class { get; set; }


        private async Task Delivery_DeliveryCostChangedHandler(DeliveryCost item)
        {
            await InvokeAsync(() => deliveryCost = item);
        }

        private async Task Delivery_DeliveryChangedHandlerAsync(SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries.UserDeliveryPoint delivery)
        {
            await InvokeAsync(() => this.delivery = delivery);
        }
    }
}
