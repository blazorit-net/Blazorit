using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Client.Shared.Routes.ECommerce.Domain
{
    public static class PageRouter
    {
        public static string RefToProductPage(CartItem cartItem)
        {
            return $"{ConstPage.PRODUCT}/{cartItem.Category}/{cartItem.ProductLinkPart}";
        }

        public static string RefToProductPage(OrderItem cartItem)
        {
            return $"{ConstPage.PRODUCT}/{cartItem.Category}/{cartItem.ProductLinkPart}";
        }
    }
}
