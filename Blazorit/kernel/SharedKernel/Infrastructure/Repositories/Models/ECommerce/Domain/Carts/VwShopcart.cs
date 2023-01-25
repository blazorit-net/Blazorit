using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts
{
    public class VwShopcart
    {
        public long? CartId { get; set; }

        public long? ProductId { get; set; }

        public string? Sku { get; set; }

        public string? Name { get; set; }

        public string? Curr { get; set; }

        public decimal? ProductPrice { get; set; }

        public string? PicLinkPart { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DateTimeModified { get; set; }
    }
}
