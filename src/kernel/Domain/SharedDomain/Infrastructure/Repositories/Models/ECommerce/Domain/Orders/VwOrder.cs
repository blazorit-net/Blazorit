namespace Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Orders
{
    public class VwOrder
    {
        public long OrderId { get; set; }

        public long DeliveryId { get; set; }

        public long ProductId { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Curr { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ProductLinkPart { get; set; } = string.Empty;

        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }
    }
}
