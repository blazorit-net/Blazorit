using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Client.Shared.Routes.ECommerce.Domain
{
    /// <summary>
    /// Methods container for references to pages with parameters
    /// </summary>
    public static class PageRouter
    {
        public static string RefToProductPage(CartItem item)
        {
            return $"{ConstPage.PRODUCT}/{item.Category}/{item.ProductLinkPart}";
        }

        public static string RefToProductPage(OrderItem item)
        {
            return $"{ConstPage.PRODUCT}/{item.Category}/{item.ProductLinkPart}";
        }
    }
}
