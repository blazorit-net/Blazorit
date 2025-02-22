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

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Curr { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ProductLinkPart { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTimeOffset DateTimeItemCreate { get; set; }

    }
}
