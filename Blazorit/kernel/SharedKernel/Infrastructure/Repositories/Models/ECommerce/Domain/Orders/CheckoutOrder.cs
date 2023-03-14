using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders
{
    public class CheckoutOrder
    {
        /// <summary>
        /// uniq token
        /// </summary>
        public string OrderToken { get; set; } = string.Empty;

        public bool Canceled { get; set; }

        public decimal PaymentAmount { get; set; }

        public long UserId { get; set; }

        public long DeliveryMethodId { get; set; }

        public long DeliveryAddressId { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
