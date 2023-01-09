using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderMenus {
    public partial class HeaderMenu {
        [Parameter]
        public string? Class { get; set; }

        //[Parameter]
        //public IList<SubMenu> MenuList { get; set; } = new List<SubMenu>();

        protected override async Task OnInitializedAsync() {
            menuItems = await dataService.GetHeaderMenu(); 
        }

        private IEnumerable<string> menuItems = new List<string>();
    }
}
