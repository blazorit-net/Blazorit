using Microsoft.AspNetCore.Components;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.Client.Shared.Routes.ECommerce.Domain;

namespace Blazorit.Client.Pages.ECommerce.Domain.Components.ProductPage.Comps.ProductCards
{
    public partial class ProductCard : IDisposable
    {
        private CartItem cartItem = new();

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;
        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;


        [Parameter] public string? Class { get; set; }

        [Parameter] public ProductCardData Data { get; set; } = new();


        protected override void OnInitialized()
        {          
            CartState.OnChange += StateHasChanged;
        }


        private bool IsProductToCartEnabled
        {
            get
            {
                if (CartState.State.CartList.FirstOrDefault(x => x.ProductId == Data.Id) == null)
                {
                    return true;
                }

                return false;
            }
        }

        //protected override void OnParametersSet()
        //{
        //    cartItem = new CartItem(Data) { Quantity = CartState.State.CartList.FirstOrDefault(x => x.ProductId == Data.Id)?.Quantity ?? 0 };
        //}


        private async Task AddToCartButton_ClickHandlerAsync() {
            await CartService.AddProductToCartAsync(new CartItem(Data) { Quantity = 1});
            /*
                //CartState.State.CartList = result.ToList();
                //CartState.NotifyStateChanged();  
            */
        }

        private async Task ShopcartButton_ClickHandler()
        {
            await InvokeAsync(() => Navigation.NavigateTo(ConstPage.SHOPCART));            
        }


        public void Dispose() {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
