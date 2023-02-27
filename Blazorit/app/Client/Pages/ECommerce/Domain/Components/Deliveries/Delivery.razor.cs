using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.Deliveries
{
    public partial class Delivery
    {
        private IEnumerable<DeliveryMethod> deliveryMethods = new List<DeliveryMethod>();
        private IEnumerable<DeliveryAddress> deliveryAddresses = new List<DeliveryAddress>();
        private int choosenDeliveryAddress = 1;

        //private long selectedMethodId;
        //DeliveryMethod selectedMethod = new();      

        [Inject]
        private IDeliveryService DeliveryService { get; set; } = null!;

        [Parameter]
        public DeliveryAddress SelectedAddress { get; set; } = new();

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public DeliveryMethod SelectedMethod { get; set; } = new();
        [Parameter]
        public EventCallback<DeliveryMethod> SelectedMethodChanged { get; set; }

        



        protected override async Task OnParametersSetAsync()
        {
            deliveryMethods = await DeliveryService.GetDeliveryMethods();
        }

        private async Task DeliveryMethod_SelectedItemChangedHandlerAsync(DeliveryMethod method)
        {
            await SelectedMethodChanged.InvokeAsync(method);
            deliveryAddresses = await DeliveryService.GetDeliveryAddresses(method.Id);
        }

        private async Task UseNewAddress_ButtonClickAsync()
        {
            await DeliveryService.AddDeliveryAddressAsync(SelectedMethod, SelectedAddress);
        }
    }
}
