using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class DeliveryAddress
    {
        public long Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public string? Comment { get; set; }
    }
}
