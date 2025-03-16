using Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Payments_Payment = Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Payments.Payment;

namespace Blazorit.Domain.SharedDomain.Core.Services.Models.ECommerce.Domain.Payments
{
    /// <summary>
    /// Payment data
    /// </summary>
    public class Payment
    {
        public Payment() { }

        public Payment(Infrastructure.Repositories.Models.ECommerce.Domain.Payments.Payment payment, PaymentMethod paymentMethod)
        {
            PaymentAmount = payment.PaymentAmount;
            IsPaid = payment.IsPaid;
            PaymentMethod = paymentMethod;
        }

        public decimal PaymentAmount { get; set; }

        public bool IsPaid { get; set; }

        public PaymentMethod PaymentMethod { get; set; } = new();

        public string StrPaymentAmount
        {
            get
            {
                return PaymentAmount.ToString("N0");
            }
        }

        public string StrIsPaid
        {
            get
            {
                if (IsPaid)
                {
                    return "paid";
                }

                return "not paid";
            }            
        }
    }
}
