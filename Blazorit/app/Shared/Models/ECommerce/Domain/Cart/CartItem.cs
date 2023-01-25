using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Shared.Models.ECommerce.Domain.Cart {
    public class CartItem {
        public string productSKU { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
