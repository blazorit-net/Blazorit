namespace Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Orders
{
    public class Order
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public long DeliveryId { get; set; }

        public long PaymentId { get; set; }
    }
}
