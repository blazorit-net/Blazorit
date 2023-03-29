using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;


namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ShopcartPage.Comps.ShopcartItems
{
    public partial class ShopcartItem
    {
        bool isImagePreviewVisible = false;
        private bool isSpinning = false; // spin on/off

        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public CartItem Item { get; set; } = null!;


        private bool isMinusButtonDisabled = false;


        protected override void OnInitialized()
        {
            if (Item.Quantity <= 1)
            {
                isMinusButtonDisabled = true;
            }
        }


        private async Task SubtractQuantity()
        {
            if (Item.Quantity > 1)
            {
                await CartService.AddProductToCartAsync(new CartItem { 
                    ProductId = Item.ProductId,
                    Sku = Item.Sku,
                    Quantity = -1
                });
            }

            if (Item.Quantity <= 1)
            {
                isMinusButtonDisabled = true;
            }
        }

        private async Task IncrementQuantity()
        {
            await CartService.AddProductToCartAsync(new CartItem
            {
                ProductId = Item.ProductId,
                Sku = Item.Sku,
                Quantity = 1
            });

            if (Item.Quantity > 1)
            {
                isMinusButtonDisabled = false;
            }
        }


        private async Task DeleteProductButton_ClickHandlerAsync(CartItem item)
        {
            isSpinning = true;
            await CartService.DeleteProductFromCartAsync(item);
            isSpinning = false;
        }
    }
}
