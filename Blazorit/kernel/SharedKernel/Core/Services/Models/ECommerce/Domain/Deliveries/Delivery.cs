using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries
{
    /// <summary>
    /// Сlass contains delivery data
    /// </summary>
    public class Delivery
    {
        public Delivery() { }

        public Delivery(UserDelivery userDelivery, DeliveryCost cost)
        {
            UserDelivery = userDelivery;
            DeliveryCost = cost;
        }

        public UserDelivery UserDelivery { get; set; } = new();

        /// <summary>
        /// Delivery cost
        /// </summary>
        public DeliveryCost DeliveryCost { get; set; } = new();    
        
        
        public bool IsCheckedDeliveryEntryFields 
        { 
            get
            {
                if (UserDelivery.AddressId != 0 && UserDelivery.MethodId != 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
