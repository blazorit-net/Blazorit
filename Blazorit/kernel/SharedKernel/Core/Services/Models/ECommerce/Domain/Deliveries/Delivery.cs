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

        public Delivery(UserDelivery userDelivery, DeliveryCost cost)
        {
            UserDelivery = userDelivery;
            DeliveryCost = cost;
        }

        public UserDelivery UserDelivery { get; set; } = new();

        public DeliveryCost DeliveryCost { get; set; } = new();        
    }
}
