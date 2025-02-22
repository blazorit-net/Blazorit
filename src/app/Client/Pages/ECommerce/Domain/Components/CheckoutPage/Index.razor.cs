using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage
{
    /// <summary>
    /// CheckoutPage
    /// </summary>
    public partial class Index : IDisposable
    {
        private UserDeliveryPoint delivery = new();

        private PaymentMethod paymentMethod = new();

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Parameter] 
        public string? Class { get; set; }


        protected override void OnInitialized()
        {
            CartState.OnChange += StateHasChanged;
        }

        private async Task Delivery_DeliveryChangedHandlerAsync(UserDeliveryPoint delivery)
        {
            await InvokeAsync(() => this.delivery = delivery);
        }


        private async Task Payment_PaymentMethodChangedHandlerAsync(PaymentMethod method)
        {
            await InvokeAsync(() => paymentMethod = method);
        }

        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
