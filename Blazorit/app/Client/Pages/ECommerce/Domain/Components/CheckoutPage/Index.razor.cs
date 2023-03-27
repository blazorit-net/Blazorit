using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage
{
    public partial class Index
    {
        private UserDeliveryPoint delivery = new();

        private PaymentMethod paymentMethod = new();

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Parameter] 
        public string? Class { get; set; }

        private async Task Delivery_DeliveryChangedHandlerAsync(UserDeliveryPoint delivery)
        {
            await InvokeAsync(() => this.delivery = delivery);
        }

        private async Task Payment_PaymentMethodChangedHandlerAsync(PaymentMethod method)
        {
            await InvokeAsync(() => paymentMethod = method);
        }
    }
}
