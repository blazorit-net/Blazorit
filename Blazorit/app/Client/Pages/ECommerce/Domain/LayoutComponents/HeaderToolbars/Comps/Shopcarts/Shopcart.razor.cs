using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.Shopcarts {
    public partial class Shopcart {
        [Inject]
        private CartState CartState { get; set; } = null!;

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
            NavigationManager.NavigateTo(ConstPage.SHOPCART);
            await IsVisibleShopcartDrawerChanged.InvokeAsync(false);
        }
    }
}
