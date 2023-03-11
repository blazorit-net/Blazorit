using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries
{
    public class UserDelivery
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long MethodId { get; set; }

        public long AddressId { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }
    }
}
