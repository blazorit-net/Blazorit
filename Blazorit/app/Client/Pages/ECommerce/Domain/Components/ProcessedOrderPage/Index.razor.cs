using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Support.Enums;
using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProcessedOrderPage
{
    /// <summary>
    /// This page is called by another 3th-d party service after payment, passing parameters about the payment that ended
    /// </summary>
    public partial class Index
    {
        private Tribool isSuccessOrder = Tribool.None;

        private Order orderData = new();

        [Inject]
        private IOrderService OrderService { get; set; } = null!;

        [Inject]
        private ICartService CartService { get; set; } = null!;

        /// <summary>
        /// Result of payment in in 3th-d party service
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "is-success")]
        public bool PaymentSuccess { get; set; } = false;

        /// <summary>
        /// Full info about payment transaction in 3th-d party service
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery] // Name as is
        public string PaymentInfo { get; set; } = string.Empty;

        /// <summary>
        /// Uniq token for order
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "token")]
        public string PaymentToken { get; set; } = string.Empty;

        /// <summary>
        /// Paid amount
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery(Name = "amount")]
        public decimal PaidAmount { get; set; }

        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await CreateOrder(); // on start this page creating order
        }


        /// <summary>
        /// Method created order
        /// </summary>
        /// <returns></returns>
        private async Task CreateOrder()
        {
            if (PaymentSuccess)
            {
                PaidOrder orderInfo = new()
                {
                    OrderToken = PaymentToken,
                    PaidAmount = PaidAmount,
                    PaymentInfo = PaymentInfo
                };

                Response<Order> orderResponse = await OrderService.CreateOrder(orderInfo);

                if (orderResponse.Ok)
                {
                    orderData = orderResponse.Data!;                    
                } 

                isSuccessOrder = orderResponse.Ok.ToTribool();
                await CartService.SyncShopCartAsync();
            }
        }
    }
}
