using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;


namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ShopcartPage
{
    public partial class Index : IDisposable
    {
        [Inject]
        private CartState CartState { get; set; } = null!;

        //[Inject]
        //private IOrderService OrderService { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }


        protected override void OnInitialized()
        {
            CartState.OnChange += StateHasChanged;
        }


        public async Task OrderButton_ClickHandlerAsync()
        {
            await InvokeAsync(() => Navigation.NavigateTo(ConstPage.DELIVERY));

            //await OrderService.CreateOrderFromCart();
        }


        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
