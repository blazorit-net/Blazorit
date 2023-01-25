using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderMenus
{
    public partial class HeaderMenu {
        private IEnumerable<SubMenu> mainMenu = new List<SubMenu>();

        [Parameter]
        public string? Class { get; set; }


        [Parameter]
        public IEnumerable<SubMenu>? MainMenu { get; set; }


        protected override async Task OnInitializedAsync() {           
            if (MainMenu != null) {
                mainMenu = MainMenu;
            } else {
                mainMenu = await dataService.GetHeaderMenu();
            }
            
            
            

            //mainMenu = await dataService.GetHeaderMenu(); 
        }      
    }
}
