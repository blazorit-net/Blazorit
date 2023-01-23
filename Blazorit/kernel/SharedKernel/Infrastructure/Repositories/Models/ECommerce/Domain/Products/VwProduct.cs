using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products
{
    public class VwProduct
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public string Curr { get; set; } = null!;

        public decimal Price { get; set; }

        public DateOnly DateCreate { get; set; }

        public DateOnly DateModified { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public DateTime DateTimeModified { get; set; }

        public string? Description { get; set; }

        public string LinkPart { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string CategoryFullName { get; set; } = null!;
    }
}
