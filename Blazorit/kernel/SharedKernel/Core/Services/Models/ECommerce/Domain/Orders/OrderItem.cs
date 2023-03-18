using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    public class OrderItem
    {
        public OrderItem() { }

        ////public OrderItem(ProductCardData product)
        ////{
        ////    ProductId = product.Id;
        ////    Sku = product.Sku;
        ////    Category = product.Category;
        ////    ProductLinkPart = product.LinkPart;
        ////    Name = product.Name;
        ////    Price = product.Price;
        ////    ProductPictureLinkPart = product.MainPictureLinkPart;
        ////    PicturesLinkParts = product.PicturesLinkParts;
        ////    Quantity = 0;
        ////}

        public long ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// real Product price
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Order price (possible, discount price)
        /// </summary>
        public decimal OrderPrice { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ProductLinkPart { get; set; } = string.Empty;

        public IEnumerable<PictureLinkPart> PicturesLinkParts { get; set; } = Enumerable.Empty<PictureLinkPart>();

        public string ProductPictureLinkPart { get; set; } = string.Empty;

        ////public DateTimeOffset DateTimeCreated { get; set; }

        /// <summary>
        /// Total product price (ProductPrice * Quantity)
        /// </summary>
        public decimal TotalProductPrice
        {
            get
            {
                return ProductPrice * Quantity;
            }
        }

        /// <summary>
        /// Product price for user view
        /// </summary>
        public string StrProductPrice
        {
            get
            {
                return ProductPrice.ToString("N0");
            }
        }

        /// <summary>
        /// Total product price (ProductPrice * Quantity) for user view
        /// </summary>
        public string StrTotalProductPrice
        {
            get
            {
                return TotalProductPrice.ToString("N0");
            }
        }


        /// <summary>
        /// Total order price (OrderPrice * Quantity)
        /// </summary>
        public decimal TotalOrderPrice
        {
            get
            {
                return OrderPrice * Quantity;
            }
        }

        /// <summary>
        /// Order price for user view
        /// </summary>
        public string StrOrderPrice
        {
            get
            {
                return ProductPrice.ToString("N0");
            }
        }

        /// <summary>
        /// Total order price (OrderPrice * Quantity) for user view
        /// </summary>
        public string StrTotalOrderPrice
        {
            get
            {
                return TotalOrderPrice.ToString("N0");
            }
        }
    }
}
