using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Shared.Models.ECommerce.Domain.Deliveries
{
    public class MethodAddress
    {
        public long MethodId { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}
