using Blazorit.Client.Providers.Concrete.Identity;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars {
    public partial class HeaderToolbar : IDisposable {
        ////CssBuilder classHeaderToolboar = BlazorComponentUtilities.CssBuilder.Default("header-toolbar").AddClass(""); 

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Inject]
        private IIdentityService IdentityService { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Inject]
        private ICartService CartService { get; set; } = null!;


        [Parameter]
        public string? Class { get; set; }



        protected override async Task OnInitializedAsync() {
            CartState.OnChange += StateHasChanged;
            if (!CartService.IsLoginingNow)
            {
                await CartService.SyncShopCartAsync(); //update shopcart state
                //CartState.State = await CartService.GetShopCartListAsync(); //update shopcart state
            }
        }


        private async Task Logout() {
            await CartService.SetLocalShopCartFromServerShopCart();
            await IdentityService.LogoutAsync();
            Navigation.NavigateTo("/", false); ////Navigation.NavigateTo("/", true);
        }


        public void Dispose() {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
