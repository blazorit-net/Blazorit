using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders
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
