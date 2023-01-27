using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.CartButtons {
    public partial class CartButton {
        private bool isVisibleCartDrawer = false;

        [Parameter] public string? Class { get; set; }

        [Parameter] public int Count { get; set; } = 0;        

        private void OpenCartDrawer() {
            this.isVisibleCartDrawer = true;
        }

        private void CloseCartDrawer() {
            this.isVisibleCartDrawer = false;
        }
    }
}
