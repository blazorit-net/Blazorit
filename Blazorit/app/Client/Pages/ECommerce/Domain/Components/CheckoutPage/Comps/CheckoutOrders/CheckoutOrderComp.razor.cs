using AntDesign;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;
using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.CheckoutOrders
{
    public partial class CheckoutOrderComp
    {
        private CheckOrder checkoutOrder = new();

        [Inject]
        private IMessageService AntMessage { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;


        [Inject]
        private IOrderService OrderService { get; set; } = null!;


        [Parameter]
        public ShopCart ShopCart { get; set; } = new();


        [Parameter]
        public SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries.UserDeliveryPoint Delivery { get; set; } = new();


        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await InvokeAsync(() => checkoutOrder = new CheckOrder(ShopCart, Delivery));
        }


        public async Task CreateOrderButton_ClickHandler()
        {
            // check params for order creating

            // if user needs to select his delivery
            if (!this.Delivery.IsCheckedDeliveryEntryFields)
            {
                await AntMessage.Warning("Please, select a delivery option");
                return;
            }

            Response<string> resultToken = await OrderService.CreateUniqOrderTokenAsync(checkoutOrder); // create token(info)

            if (resultToken.Ok)
            {
                // TODO: go to 3th-d party payment service and recive it token-returned
                Navigation.NavigateTo($"{ConstPage.PAYMENT}/{resultToken.Data}/{checkoutOrder.TotalPrice}");
            }




            //// this func must implement in other place
            //if (resultToken.Ok)
            //{
            //    PaidOrder orderInfo = new()
            //    {
            //        OrderToken = resultToken.Data!,
            //        PaidAmount = 0,
            //        PaymentInfo = "some payment info"
            //    };
            //await OrderService.CreateOrder(orderInfo);
        }
        
    }
}
