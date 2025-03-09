namespace Blazorit.Domain.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class DeliveryAddress
    {
        public long Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
