using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.SiteContents {
    public partial class SiteContent {
        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public RenderFragment? Body { get; set; }
    }
}
