using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards
{
    public class ProductCardData
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string LinkPart { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string CategoryFullName { get; set; } = null!;

        public IEnumerable<PictureLinkPart> PicturesLinkParts { get; set; } = Enumerable.Empty<PictureLinkPart>();

        public string MainPictureLinkPart { get; set; } = null!;
    }
}
