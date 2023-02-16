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
        bool isImagePreviewVisible = false;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        [Inject]
        private ICartService CartService { get; set; } = null!;

        [Inject]
        private CartState CartState { get; set; } = null!;


        [Parameter] 
        public string? Class { get; set; }

        [Parameter] 
        public ProductCardData Data { get; set; } = new();


        protected override void OnInitialized()
        {          
            CartState.OnChange += StateHasChanged;
        }

        /// <summary>
        /// Button "Add to Cart" flag
        /// </summary>
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


        private async Task AddToCartButton_ClickHandlerAsync() {
            await CartService.AddProductToCartAsync(new CartItem(Data) { Quantity = 1});
            /*
                //CartState.State.CartList = result.ToList();
                //CartState.NotifyStateChanged();  
            */
        }

        private async Task ShopcartButton_ClickHandlerAsync()
        {
            await InvokeAsync(() => Navigation.NavigateTo(ConstPage.SHOPCART));            
        }


        public void Dispose() 
        {
            CartState.OnChange -= StateHasChanged;
        }
    }
}
