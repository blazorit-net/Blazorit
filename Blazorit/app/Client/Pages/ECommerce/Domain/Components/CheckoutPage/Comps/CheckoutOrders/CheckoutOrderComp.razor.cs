using AntDesign;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;
using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.CheckoutOrders
{
    public partial class CheckoutOrderComp
    {
        private bool isSpinning = false; // spin on/off
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
        public UserDeliveryPoint Delivery { get; set; } = new();

        [Parameter]
        public PaymentMethod PaymentMethod { get; set; } = new();


        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await InvokeAsync(() => checkoutOrder = new CheckOrder(ShopCart, Delivery, PaymentMethod));
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

            // if user needs to select his delivery
            if (!this.PaymentMethod.IsOkMethod)
            {
                await AntMessage.Warning("Please, select a payment method");
                return;
            }

            isSpinning = true;
            Response<string> resultToken = await OrderService.CreateUniqOrderTokenAsync(checkoutOrder); // create token(info)
            isSpinning = false;

            if (resultToken.Ok)
            {
                if (PaymentMethod.IsCOD) // cash on delivery method
                {
                    // redirect to url (our) with our token and other info about payment
                    string uri = Navigation.GetUriWithQueryParameters($"{ConstPage.PROCESSED_ORDER}", new Dictionary<string, object?>
                    {
                        ["is-success"] = true,
                        ["PaymentInfo"] = "cash on delivery",
                        ["token"] = resultToken.Data,
                        ["amount"] = 0
                    });

                    Navigation.NavigateTo(uri);
                }
                else // online paying method
                {
                    // TODO: go to 3th-d party payment service and recive it token-returned
                    Navigation.NavigateTo($"{ConstPage.PAYMENT}/{resultToken.Data}/{checkoutOrder.TotalPrice}");
                }
                // else // await AntMessage.Warning("Error. This payment method is not supported");
            } 
            else
            {
                await AntMessage.Warning("Error. Try to go back to the cart and checkout again");
            }
        }        
    }
}
