namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders
{
    public interface IOrderService
    {
        Task<bool> CreateOrderFromCart(long userId);
    }
}
