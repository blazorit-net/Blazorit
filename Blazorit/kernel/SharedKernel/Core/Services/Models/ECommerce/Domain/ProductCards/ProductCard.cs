using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards {
    public class ProductCard {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public string LinkPart { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string CategoryFullName { get; set; } = null!;
    }
}
