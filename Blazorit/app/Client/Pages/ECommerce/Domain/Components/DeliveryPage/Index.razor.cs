using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.DeliveryPage
{
    public partial class Index
    {
        private IEnumerable<DeliveryMethod> deliveryMethods = new List<DeliveryMethod>();
        private IEnumerable<DeliveryAddress> deliveryAddresses = new List<DeliveryAddress>();

        private long selectedMethodId;
        private long selectedAddressId;

        [Parameter] 
        public string? Class { get; set; }

        [Inject]
        private IDeliveryService DeliveryService { get; set; } = null!;


        protected override async Task OnParametersSetAsync()
        {
            deliveryMethods = await DeliveryService.GetDeliveryMethods();
        }

        private async Task DeliveryMethod_SelectedItemChangedHandlerAsync(DeliveryMethod method)
        {
            deliveryAddresses = await DeliveryService.GetDeliveryAddresses(method.Id);
        }
    }
}
