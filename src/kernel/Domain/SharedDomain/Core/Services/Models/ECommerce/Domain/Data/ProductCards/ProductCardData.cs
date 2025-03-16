using Blazorit.Domain.SharedDomain.Infrastructure.Repositories.Models.ECommerce.Domain.Products;

namespace Blazorit.Domain.SharedDomain.Core.Services.Models.ECommerce.Domain.Data.ProductCards
{
    public class ProductCardData
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string LinkPart { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CategoryFullName { get; set; } = string.Empty;

        public IEnumerable<PictureLinkPart> PicturesLinkParts { get; set; } = Enumerable.Empty<PictureLinkPart>();

        public string MainPictureLinkPart { get; set; } = string.Empty;

        /// <summary>
        /// Price for user view
        /// </summary>
        public string StrPrice
        {
            get
            {
                return Price.ToString("N0");
            }
        }
    }
}
