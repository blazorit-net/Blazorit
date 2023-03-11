using AntDesign;
using Blazorit.Client.Models.ECommerce.Domain.Deliveries;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;
using KernelDeliveries = Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.Deliveries
{
    public partial class Delivery
    {
        private IEnumerable<DeliveryMethod> deliveryMethods = new List<DeliveryMethod>();
        private IEnumerable<DeliveryAddress> deliveryAddresses = new List<DeliveryAddress>();
        private DeliveryAddressRadio choosenDeliveryAddressRadio = DeliveryAddressRadio.ExistingDeliveryAddresses;
        private string deliveryTextArea = string.Empty;
        //private DeliveryCost deliveryCost = new();
        private KernelDeliveries.Delivery delivery = new();

        //private Select<DeliveryAddress, DeliveryAddress>? SelectDeliveryAddressRef = new();

        [Inject]
        private IDeliveryService DeliveryService { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public DeliveryAddress SelectedAddress { get; set; } = new();
        //[Parameter]
        //public EventCallback<DeliveryAddress> SelectedAddressChanged { get; set; } = new();

        [Parameter]
        public DeliveryMethod SelectedMethod { get; set; } = new();
        //[Parameter]
        //public EventCallback<DeliveryMethod> SelectedMethodChanged { get; set; }


        //[Parameter]
        //public EventCallback<DeliveryCost> DeliveryCostChanged { get; set; } = new();


        [Parameter]
        public EventCallback<KernelDeliveries.Delivery> OnDeliveryChanged { get; set; } = new();



        protected override async Task OnInitializedAsync()
        {
            deliveryMethods = await DeliveryService.GetDeliveryMethods();
            SelectedMethod = deliveryMethods.OrderBy(x => x.Id).FirstOrDefault() ?? new();
            
            deliveryAddresses = await DeliveryService.GetDeliveryAddresses(SelectedMethod);

            if (SelectedMethod.EnterAddress && deliveryAddresses.Count() == 0)
            {
                choosenDeliveryAddressRadio = DeliveryAddressRadio.NewDeliveryAddresses;
            }
            else
            {
                choosenDeliveryAddressRadio = DeliveryAddressRadio.ExistingDeliveryAddresses;
            }

            //await SelectedMethodChanged.InvokeAsync(SelectedMethod);
            delivery.UserDelivery.MethodId = SelectedMethod.Id;
            await OnDeliveryChanged.InvokeAsync(delivery);
        }

  
        private async Task DeliveryMethod_SelectedItemChangedHandlerAsync(DeliveryMethod method)
        {
            SelectedAddress.Address = string.Empty;
            deliveryAddresses = await DeliveryService.GetDeliveryAddresses(method);            

            if (method.EnterAddress && deliveryAddresses.Count() == 0)
            {
                choosenDeliveryAddressRadio = DeliveryAddressRadio.NewDeliveryAddresses;
            }
            else
            {
                choosenDeliveryAddressRadio = DeliveryAddressRadio.ExistingDeliveryAddresses;
            }

            delivery.UserDelivery.MethodId = method.Id;
            delivery.DeliveryCost = new DeliveryCost(); // reset delivery cost
            await OnDeliveryChanged.InvokeAsync(delivery); 
            //await SelectedMethodChanged.InvokeAsync(method);
        }

        private async Task UseNewAddress_ButtonClickAsync()
        {
            deliveryAddresses = await DeliveryService.AddDeliveryAddressAsync(SelectedMethod, deliveryTextArea);
            if (deliveryAddresses.Count() != 0)
            {
                SelectedAddress = deliveryAddresses.LastOrDefault() ?? new();
                choosenDeliveryAddressRadio = DeliveryAddressRadio.ExistingDeliveryAddresses;
            }

            deliveryTextArea = string.Empty;
        }


        private async Task DeliveryAddressesSelect_SelectedItemChangedHandler(DeliveryAddress selectedAddress)
        {
            delivery.UserDelivery.AddressId = selectedAddress.Id;
            delivery.DeliveryCost = await DeliveryService.GetDeliveryCost(SelectedMethod, selectedAddress); ;
            //await SelectedAddressChanged.InvokeAsync(selectedAddress);
            await OnDeliveryChanged.InvokeAsync(delivery);
            

            //delivery = new KernelDeliveries.Delivery (
            //    new UserDelivery {
            //        MethodId = SelectedMethod.Id,
            //        AddressId = selectedAddress.Id
            //    }
            //    , deliveryCost
            //);

            //await OnDeliveryChanged.InvokeAsync(delivery);
        }
    }
}
