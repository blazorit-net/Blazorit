using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products
{
    public class Product
    {
        public Product() { }

        public Product(VwProduct product) 
        { 
            this.Id = product.Id;
            this.Sku = product.Sku;
            this.Name = product.Name;
            this.Curr = product.Curr;
            this.Price = product.Price;
            this.DateTimeCreate = product.DateTimeCreate;
            this.DateTimeModified = product.DateTimeModified;
            this.Description = product.Description;
            this.LinkPart = product.LinkPart;
            this.Category = product.Category;
            this.CategoryFullName = product.CategoryFullName;
        }

        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public string Curr { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTimeOffset DateTimeCreate { get; set; }

        public DateTimeOffset DateTimeModified { get; set; }

        public string Description { get; set; } = string.Empty;

        public string LinkPart { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CategoryFullName { get; set; } = string.Empty;
    }
}
