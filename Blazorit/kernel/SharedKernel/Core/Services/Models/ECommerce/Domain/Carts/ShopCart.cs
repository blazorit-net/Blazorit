
namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts {
    public class ShopCart {

        public ShopCart() { }

        public ShopCart(IEnumerable<CartItem> cartList) {
            CartList = cartList.ToList();
        }

        public List<CartItem> CartList { get; set; } = new List<CartItem>();

        public int TotalQuantity {
            get {
                return CartList.Sum(x => x.Quantity);
            }
        }

        public decimal TotalPrice {
            get {
                return CartList.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}
