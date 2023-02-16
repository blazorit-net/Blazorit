namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders
{
    public interface IOrderService
    {
        Task CreateOrderFromCart();
    }
}
