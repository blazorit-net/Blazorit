using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class VwDelivery
    {
        public long Id { get; set; }

        public DateTimeOffset DateTimeCreate { get; set; }

        public long UserId { get; set; }

        public long MethodId { get; set; }

        public long AddressId { get; set; }

        public string Method { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public decimal DeliveryCost { get; set; }

        public DateOnly DeliveryDate { get; set; } = default;

        public DateTimeOffset DeliveryTimeStart { get; set; } = default;

        public DateTimeOffset DeliveryTimeEnd { get; set; } = default;
    }
}
