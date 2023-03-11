using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.CheckoutOrders
{
    public partial class CheckoutOrderComp
    {
        private CheckoutOrder checkoutOrder = new();

        [Parameter]
        public ShopCart ShopCart { get; set; } = new();

        //[Parameter]
        //public DeliveryCost DeliveryCost { get; set; } = new();

        [Parameter]
        public Delivery Delivery { get; set; } = new();


        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await InvokeAsync(() => checkoutOrder = new CheckoutOrder(ShopCart, Delivery));
        }


        public async Task CreateOrderButton_ClickHandler()
        {

        }
    }
}
