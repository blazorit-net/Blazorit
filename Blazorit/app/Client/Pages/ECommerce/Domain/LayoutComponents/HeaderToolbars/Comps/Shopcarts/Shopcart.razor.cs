using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using AntDesign;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.Shopcarts {
    public partial class Shopcart : IDisposable {
        private bool isSpinning = false; // spin on/off

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Inject]
        private IMessageService AntMessage { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public bool IsVisibleShopcartDrawer { get; set; } = false;

        [Parameter]
        public EventCallback<bool> IsVisibleShopcartDrawerChanged { get; set; }

        protected override void OnInitialized() {
            CartState.OnChange += StateHasChanged;
        }

        private async Task ShopcartButton_ClickHandler()
        {
            if (CartState.State.TotalQuantity == 0)
            {
                await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
                await AntMessage.Warning("Your cart is empty");                
                return;
            }

            isSpinning = true;
            NavigationManager.NavigateTo(ConstPage.SHOPCART);
            await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
            isSpinning = false;
        }

        private async Task CheckoutButton_ClickHandler()
        {
            if (CartState.State.TotalQuantity == 0)
            {                
                await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
                await AntMessage.Warning("Your cart is empty");
                return;
            }

            isSpinning = true;
            await InvokeAsync(() => NavigationManager.NavigateTo(ConstPage.CHECKOUT));
            await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
            isSpinning = false;
        }


        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
