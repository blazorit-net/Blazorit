using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Models.ECommerce.Domain.Carts {
    public class ShopCart {

        public ShopCart() { }

        public ShopCart(IEnumerable<VwShopcart> cartList) {
            CartList = cartList.ToList();
        }

        public List<VwShopcart> CartList { get; set; } = new List<VwShopcart>();

        public int TotalQuantity {
            get {
                return CartList.Sum(x => x.Quantity);
            }
        }
    }
}
