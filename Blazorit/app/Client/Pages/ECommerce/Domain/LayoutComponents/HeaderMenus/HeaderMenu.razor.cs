﻿using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderMenus {
    public partial class HeaderMenu {
        private IEnumerable<SubMenu> mainMenu = new List<SubMenu>();

        [Parameter]
        public string? Class { get; set; }


        protected override async Task OnInitializedAsync() {
            mainMenu = await dataService.GetHeaderMenu(); 
        }      
    }
}
