using Blazorit.Client.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.Payments
{
    public partial class Payment
    {
        private IEnumerable<PaymentMethod> paymentMethods = new List<PaymentMethod>();
        private PaymentMethod selectedMethod = new();

        [Inject]
        private IPaymentService PaymentService { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public EventCallback<PaymentMethod> OnPaymentMethodChanged { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            paymentMethods = await PaymentService.GetPaymentMethodsAsync();
            selectedMethod = paymentMethods.FirstOrDefault() ?? new();
            await OnPaymentMethodChanged.InvokeAsync(selectedMethod);
        }

        public async Task PaymentMethod_SelectedItemChangedHandlerAsync(PaymentMethod method)
        {
            await OnPaymentMethodChanged.InvokeAsync(method);
        }
    }
}
