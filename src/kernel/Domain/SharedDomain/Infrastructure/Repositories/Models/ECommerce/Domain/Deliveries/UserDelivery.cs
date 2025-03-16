namespace Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    /// <summary>
    /// Customer delivery point
    /// </summary>
    public class UserDelivery
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long MethodId { get; set; }

        public long AddressId { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
