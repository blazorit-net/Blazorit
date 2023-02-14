using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Shared.Routes.ECommerce.Domain
{
    public static class PageRouter
    {
        public static string RefToProductPage(CartItem cartItem)
        {
            return $"{ConstPage.PRODUCT}/{cartItem.Category}/{cartItem.ProductLinkPart}";
        }
    }
}
