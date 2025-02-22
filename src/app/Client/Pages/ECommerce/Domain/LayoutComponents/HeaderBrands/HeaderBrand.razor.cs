using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderBrands {
    public partial class HeaderBrand {
        [Parameter]
        public string? Class { get; set; }

        private string? txtValue { get; set; }

        public async Task SearchHandler() {
            await message.Loading($"searching {txtValue}", 2);
        }
    }
}
