using Blazorit.Client.Providers.Concrete.Identity;
using Microsoft.AspNetCore.Components;
using Blazorit.Client.Shared.Routes.ECommerce.Admin;

namespace Blazorit.Client.Pages.ECommerce.Admin.LayoutComponents.HeaderToolbars
{
    public partial class HeaderToolbar {
        [Parameter]
        public string? Class { get; set; }

        private void NavigateToLogin() {
            Navigation.NavigateTo(AdminPage.ADMIN_LOGIN);
        }

        //private async Task Logout() {
        //    ////var authProvider = AuthenticationStateProvider as CustomAuthStateProvider;
        //    ////if (authProvider == null) return;
        //    ////await authProvider.LogoutAuthenticationStateAsync();
        //    ////Navigation.NavigateTo("/", false); ////Navigation.NavigateTo("/", true);
        //}

        ////CssBuilder classHeaderToolboar = BlazorComponentUtilities.CssBuilder.Default("header-toolbar").AddClass("");
    }
}
