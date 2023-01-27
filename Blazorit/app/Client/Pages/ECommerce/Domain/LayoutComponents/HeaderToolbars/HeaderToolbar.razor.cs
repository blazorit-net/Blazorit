using Blazorit.Client.Providers.Concrete.Identity;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;
using Blazorit.Client.Providers.Concrete.Identity;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars {
    public partial class HeaderToolbar : IDisposable {
        ////CssBuilder classHeaderToolboar = BlazorComponentUtilities.CssBuilder.Default("header-toolbar").AddClass(""); 

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;


        [Parameter]
        public string? Class { get; set; }



        protected override void OnInitialized() {
            CartState.OnChange += StateHasChanged;
        }


        private async Task Logout() {
            var authProvider = AuthenticationStateProvider as CustomAuthStateProvider;
            if (authProvider == null) return;
            await authProvider.LogoutAuthenticationStateAsync();
            Navigation.NavigateTo("/", false); ////Navigation.NavigateTo("/", true);
        }


        public void Dispose() {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
