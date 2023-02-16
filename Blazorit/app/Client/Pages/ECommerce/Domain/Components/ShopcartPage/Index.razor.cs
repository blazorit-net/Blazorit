using Blazorit.Client.States.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;


namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ShopcartPage
{
    public partial class Index : IDisposable
    {
        [Inject]
        private CartState CartState { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }


        protected override void OnInitialized()
        {
            CartState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
