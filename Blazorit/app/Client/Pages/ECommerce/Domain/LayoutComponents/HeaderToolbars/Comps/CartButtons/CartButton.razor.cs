using AntDesign;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.CartButtons {
    public partial class CartButton {
        private bool isVisibleCartDrawer = false;

        [Inject]
        private IMessageService AntMessage { get; set; } = null!;

        [Parameter] public string? Class { get; set; }

        [Parameter] public int Count { get; set; } = 0;        

        private async Task OpenCartDrawerAsync() {
            if (Count == 0)
            {
                await AntMessage.Info("Your cart is empty");
                return;
            }

            this.isVisibleCartDrawer = true;
        }

        private void CloseCartDrawer() {
            this.isVisibleCartDrawer = false;
        }
    }
}
