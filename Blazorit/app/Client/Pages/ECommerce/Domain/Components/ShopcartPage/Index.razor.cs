using AntDesign;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;


namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ShopcartPage
{
    /// <summary>
    /// ShopcartPage
    /// </summary>
    public partial class Index : IDisposable
    {
        [Inject]
        private CartState CartState { get; set; } = null!;

        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Inject]
        private IMessageService AntMessage { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnInitializedAsync()
        {
            CartState.OnChange += StateHasChanged;
            await CartService.SyncShopCartAsync(); // sync cart. because, in another browser tab, cart itmes could be changed
        }


        public async Task CheckoutButton_ClickHandlerAsync()
        {

            if (CartState.State.TotalQuantity == 0)
            {                
                await AntMessage.Warning("Your cart is empty");
                return;
            }

            await InvokeAsync(() => Navigation.NavigateTo(ConstPage.CHECKOUT));
        }


        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
