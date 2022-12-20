using Blazorit.Client.Shared.Routes.ECommerce;
using Blazorit.Client.Providers.Concrete.Identity;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Admin.HeaderToolbars {
    public partial class HeaderToolbar {
        [Parameter]
        public string? Class { get; set; }

        private void NavigateToLogin() {
            Navigation.NavigateTo(ConstPage.ADMIN_LOGIN);
        }

        private async Task Logout() {
            var authProvider = AuthenticationStateProvider as CustomAuthStateProvider;
            if (authProvider == null) return;
            await authProvider.LogoutAuthenticationStateAsync();
            Navigation.NavigateTo("/", false); ////Navigation.NavigateTo("/", true);
        }

        ////CssBuilder classHeaderToolboar = BlazorComponentUtilities.CssBuilder.Default("header-toolbar").AddClass("");
    }
}
