using Microsoft.AspNetCore.Components;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Cart;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage.Comps.ProductCards
{
    public partial class ProductCard
    {
        [Parameter] public string? Class { get; set; }

        [Parameter] public ProductCardData Data { get; set; } = new();

        [Inject]
        private ICartService CartService { get; set; } = null!;


        private async Task AddToCartButtonClickHandlerAsync() {
            var result = await CartService.AddProductToCartAsync(Data.Sku, 1);
        }
    }
}
