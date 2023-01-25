using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Cart
{
    public interface ICartService
    {
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity);
    }
}
