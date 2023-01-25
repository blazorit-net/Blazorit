using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Cart
{
    public interface ICartService
    {
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(string productSKU, int quantity);
    }
}
