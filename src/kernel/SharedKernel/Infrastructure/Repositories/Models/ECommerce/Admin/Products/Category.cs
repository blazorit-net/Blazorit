using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PrefixSku { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}
