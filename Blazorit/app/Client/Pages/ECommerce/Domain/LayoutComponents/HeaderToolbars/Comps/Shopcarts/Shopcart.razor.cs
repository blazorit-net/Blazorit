using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.Shopcarts {
    public partial class Shopcart {

        [Inject]
        private CartState CartState { get; set; } = null!;


        protected override void OnInitialized() {
            CartState.OnChange += StateHasChanged;
        }

    }
}
