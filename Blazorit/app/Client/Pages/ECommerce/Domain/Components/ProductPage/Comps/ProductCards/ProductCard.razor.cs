using Microsoft.AspNetCore.Components;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage.Comps.ProductCards
{
    public partial class ProductCard
    {
        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;


        [Parameter] public string? Class { get; set; }

        [Parameter] public ProductCardData Data { get; set; } = new();


        private async Task AddToCart_ButtonClickHandlerAsync() {
            CartState.State = await CartService.AddProductToCartAsync(new CartItem(Data) { Quantity = 1});
            /*
            //CartState.State.CartList = result.ToList();
            //CartState.NotifyStateChanged();  
            */
        }
    }
}
