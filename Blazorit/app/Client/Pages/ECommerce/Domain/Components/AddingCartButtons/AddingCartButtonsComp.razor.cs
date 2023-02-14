using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.AddingCartButtons
{
    public partial class AddingCartButtonsComp : IDisposable
    {
        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public long ProductId { get; set; }


        private bool isMinusButtonDisabled = false;

        private CartItem CartItem
        {
            get
            {
                return CartState.State.CartList.FirstOrDefault(x => x.ProductId == ProductId) ?? new();
            }
        }


        protected override void OnInitialized()
        {
            CartState.OnChange += StateHasChanged;           
        }

        protected override void OnParametersSet()
        {
            if (CartItem.Quantity <= 1)
            {
                isMinusButtonDisabled = true;
            } else
            {
                isMinusButtonDisabled = false;
            }
        }

        private async Task SubtractQuantity()
        {
            if (CartItem.Quantity > 1)
            {
                await CartService.AddProductToCartAsync(new CartItem
                {
                    ProductId = CartItem.ProductId,
                    Sku = CartItem.Sku,
                    Quantity = -1
                });
            }

            if (CartItem.Quantity <= 1)
            {
                isMinusButtonDisabled = true;
            }
        }

        private async Task IncrementQuantity()
        {
            await CartService.AddProductToCartAsync(new CartItem
            {
                ProductId = CartItem.ProductId,
                Sku = CartItem.Sku,
                Quantity = 1
            });

            if (CartItem.Quantity > 1)
            {
                isMinusButtonDisabled = false;
            }
        }

        public void Dispose()
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
