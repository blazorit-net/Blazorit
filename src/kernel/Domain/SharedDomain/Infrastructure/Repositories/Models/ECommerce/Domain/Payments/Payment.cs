namespace Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Payments
{
    public class Payment
    {
        public long Id { get; set; }

        public decimal PaymentAmount { get; set; }

        public DateTimeOffset DateTimeCreate { get; set; }

        public long CheckoutOrderId { get; set; }

        public string OrderToken { get; set; } = string.Empty;

        public string PaymentInfo { get; set; } = string.Empty;

        public bool IsPaid { get; set; }

        public long PaymentMethodId { get; set; }
    }
}
