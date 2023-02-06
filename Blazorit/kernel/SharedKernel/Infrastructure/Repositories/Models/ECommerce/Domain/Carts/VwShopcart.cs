using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts
{
    public class VwShopcart
    {
        public long CartId { get; set; }

        public long ProductId { get; set; }

        public string Sku { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Curr { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string Category { get; set; } = null!;

        public string ProductLinkPart { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }

    }
}
