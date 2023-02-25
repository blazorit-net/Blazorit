using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class DeliveryMethod
    {
        public DeliveryMethod() { }

        public long Id { get; set; }

        public string Method { get; set; } = string.Empty;
    }
}
