using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
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
        private IOrderService OrderService { get; set; } = null!;


        [Parameter]
        public ShopCart ShopCart { get; set; } = new();


        [Parameter]
        public Delivery Delivery { get; set; } = new();


        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await InvokeAsync(() => checkoutOrder = new CheckOrder(ShopCart, Delivery));
        }


        public async Task CreateOrderButton_ClickHandler()
        {
            var resultToken = await OrderService.CreateUniqOrderTokenAsync(checkoutOrder);
            // go to 3th-d party payment service and recive it token-returned

            // this func must implement in other place
            if (resultToken.Ok)
            {
                OrderCreation orderInfo = new()
                {
                    OrderToken = resultToken.Data!,
                    PaymentAmount = 0,
                    PaymentInfo = "some payment info"
                };

                await OrderService.CreateOrder(orderInfo);
            }
        }
    }
}
