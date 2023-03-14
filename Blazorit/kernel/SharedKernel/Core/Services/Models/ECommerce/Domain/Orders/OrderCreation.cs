using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    public class OrderCreation
    {
        public long UserId { get; set; }

        public string OrderToken { get; set; } = string.Empty;

        public decimal PaymentAmount { get; set; }
        
        public string PaymentInfo { get; set; } = string.Empty;
    }
}
