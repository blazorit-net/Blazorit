using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Admin.LayoutComponents.SiderMenus {
    public partial class SiderMenu {
        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public EventCallback<bool> OnCollapse { get; set; }


        private bool collapsed;      


        private void HandleCollapse(bool collapsed) {
            //Console.WriteLine(collapsed);
            this.collapsed = collapsed;
            OnCollapse.InvokeAsync(collapsed);
        }
    }
}
