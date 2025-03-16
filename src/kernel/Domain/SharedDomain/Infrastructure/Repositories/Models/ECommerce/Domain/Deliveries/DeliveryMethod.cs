namespace Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class DeliveryMethod
    {
        public DeliveryMethod() { }

        public long Id { get; set; }

        public string Method { get; set; } = string.Empty;

        public bool EnterAddress { get; set; }
    }
}
