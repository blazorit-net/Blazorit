﻿using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts {
    public class CartItem {
        public CartItem() { }

        public CartItem(ProductCardData product) 
        {
            ProductId = product.Id;
            Sku = product.Sku;
            Category = product.Category;
            ProductLinkPart = product.LinkPart;            
            Name = product.Name;
            Price = product.Price;
            ProductPictureLinkPart = product.MainPictureLinkPart;
            PicturesLinkParts = product.PicturesLinkParts;
            Quantity = 0;
        }

        public long ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ProductLinkPart { get; set; } = string.Empty;

        public IEnumerable<PictureLinkPart> PicturesLinkParts { get; set; } = Enumerable.Empty<PictureLinkPart>();

        public string ProductPictureLinkPart { get; set; } = string.Empty;

        public DateTimeOffset DateTimeCreated { get; set; }

        /// <summary>
        /// Total price (Price * Quantity)
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }

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

        /// <summary>
        /// Total price (Price * Quantity) for user view
        /// </summary>
        public string StrTotalPrice
        {
            get
            {
                return TotalPrice.ToString("N0");
            }
        }

    }
}
