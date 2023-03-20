using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries
{
    public class Delivery
    {
        public Delivery() { }

        public Delivery(VwDelivery delivery)
        {
            MethodId = delivery.MethodId;
            AddressId = delivery.AddressId;
            Method = delivery.Method;
            Address = delivery.Address;
            Comment = delivery.Comment;
            DeliveryCost = new DeliveryCost(delivery.DeliveryCost);
            DeliveryDate = delivery.DeliveryDate;
            DeliveryTimeStart = delivery.DeliveryTimeStart;
            DeliveryTimeEnd = delivery.DeliveryTimeEnd;
        }

        public long MethodId { get; set; }

        public long AddressId { get; set; }

        public string Method { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public DeliveryCost DeliveryCost { get; set; } = new();

        public DateOnly DeliveryDate { get; set; } = default;

        public DateTimeOffset DeliveryTimeStart { get; set; } = default;

        public DateTimeOffset DeliveryTimeEnd { get; set; } = default;
    }
}
