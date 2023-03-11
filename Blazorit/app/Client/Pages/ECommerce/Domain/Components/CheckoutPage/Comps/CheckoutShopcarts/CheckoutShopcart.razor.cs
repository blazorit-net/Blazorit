using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.CheckoutPage.Comps.CheckoutShopcarts
{
    public partial class CheckoutShopcart
    {
        //[Inject]
        //private CartState CartState { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }


        [Parameter]
        public ShopCart ShopCart { get; set; } = new();


        //protected override void OnInitialized()
        //{
        //    CartState.OnChange += StateHasChanged;
        //}


        //public void Dispose()
        //{
        //    CartState.OnChange -= StateHasChanged;
        //}
    }
}
