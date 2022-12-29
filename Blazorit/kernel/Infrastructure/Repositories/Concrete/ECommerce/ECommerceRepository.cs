using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;

namespace Blazorit.Infrastructure.Repositories.Concrete.ECommerce {
    public class ECommerceRepository : IECommerceRepository {
        private readonly IDbContextFactory<BlazoritContext> _contextFactory;


        public ECommerceRepository(IDbContextFactory<BlazoritContext> contextFactory) {
            _contextFactory = contextFactory;
        }


        public async Task<bool> AddProductToCart(long userId, string productSKU, int quantity) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = context.ProdProducts.Where(prod => prod.Id == 7).FirstOrDefault();
                //VwProdProduct? product = context.VwProdProducts.Where(prod => prod.Sku == productSKU)..FirstOrDefault();

                if (product == null) {
                    return false;
                }

                CartShopcart cart = new CartShopcart() {
                    UserId = userId,
                };
                CartShopcartList cartList = new CartShopcartList() {
                    Cart = cart,
                    //CartId = cart.Id,
                    Product = product,
                    //ProductId = product.Id,
                    Quantity = quantity
                };

                
                context.CartShopcartLists.Add(cartList);
                context.CartShopcarts.Add(cart);
                await context.SaveChangesAsync();

                return true;
            } catch (Exception ex) {

            }
            return false;
        }
    }
}
