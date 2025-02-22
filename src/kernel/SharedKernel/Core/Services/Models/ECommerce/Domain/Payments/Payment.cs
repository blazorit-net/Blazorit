using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrPayments = Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Payments
{
    /// <summary>
    /// Payment data
    /// </summary>
    public class Payment
    {
        public Payment() { }

        public Payment(InfrPayments.Payment payment, PaymentMethod paymentMethod)
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
