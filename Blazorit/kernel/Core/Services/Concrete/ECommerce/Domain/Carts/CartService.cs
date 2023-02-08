using Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Carts {
    /// <summary>
    /// Cart service for shopcarts
    /// </summary>
    public class CartService : ICartService {
        private readonly IECommerceRepository _dataRepo;

        public CartService(IECommerceRepository dataRepo) {
            _dataRepo = dataRepo;
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShopCart?> GetShopCartListAsync(long userId) {
            IEnumerable<VwShopcart> repoResult = await _dataRepo.GetShopCartListAsync(userId);
            IEnumerable<CartItem> result = await GetCartItemsFromShopcartsAsync(repoResult);
            return result.Count() == 0 ? null : new ShopCart(result);
        }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart by SKU
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        public async Task<ShopCart?> AddProductToCartAsync(long userId, string productSKU, int quantity) {
            var response = await _dataRepo.AddProductToCartAsync(userId, productSKU, quantity);
            return await GetShopCartListAsync(userId);
        }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart by productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<ShopCart?> AddProductToCartAsync(long userId, long productId, int quantity)
        {
            var response = await _dataRepo.AddProductToCartAsync(userId, productId, quantity);
            return await GetShopCartListAsync(userId);
        }


        /// <summary>
        /// Method merges client shopcart with kernel cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientCart"></param>
        /// <returns>Result cart</returns>
        public async Task<ShopCart?> MergeShopCarts(long userId, ShopCart clientCart) {
            IEnumerable<VwShopcart> repoList = await _dataRepo.GetShopCartListAsync(userId);
            IEnumerable<CartItem> clientList = clientCart.CartList;

            var repoOuterJoin =
               from left in repoList
               join right in clientList on left.ProductId equals right.ProductId into temp
               from right in temp.DefaultIfEmpty()
               select new 
               {
                   ProductId = left.ProductId,
                   Quantity = Math.Max(left.Quantity, right?.Quantity ?? 0), //get max quantity
                   DateTimeCreated = right?.DateTimeCreated ?? left.DateTimeCreated
               };
            var clientOuterJoin =
                from left in clientList
                join right in repoList on left.ProductId equals right.ProductId into temp
                from right in temp.DefaultIfEmpty()
                select new 
                {
                      ProductId = left.ProductId,
                      Quantity = Math.Max(left.Quantity, right?.Quantity ?? 0), //left.Quantity > (right?.Quantity ?? 0) ? left.Quantity : right.Quantity,
                      DateTimeCreated = left.DateTimeCreated
                };
            
            var fullOuterJoin = repoOuterJoin.Union(clientOuterJoin).Select(x => new VwShopcart 
            {
                  ProductId= x.ProductId,
                  Quantity = x.Quantity,
                  DateTimeCreated = x.DateTimeCreated
            }).ToList();

            var repoResult = await _dataRepo.UpdateShopCart(userId, fullOuterJoin);
            IEnumerable<CartItem> result = await GetCartItemsFromShopcartsAsync(repoResult);
            return result.Count() == 0 ? null : new ShopCart(result);
        }


        /// <summary>
        /// Method converts IEnumerable<VwShopcart> to IEnumerable<CartItem> and adds additional data from repository (picture's link parts)
        /// </summary>
        /// <param name="shopcartList"></param>
        /// <returns></returns>
        private async Task<IEnumerable<CartItem>> GetCartItemsFromShopcartsAsync(IEnumerable<VwShopcart> shopcartList) {

            IEnumerable<CartItem> result = shopcartList.Select(x => new CartItem {
                ProductId = x.ProductId,
                ProductLinkPart = x.ProductLinkPart.Trim(),
                Name = x.Name,
                Price = x.ProductPrice,
                Sku = x.Sku,
                Quantity = x.Quantity
            }).ToList();

            foreach (var item in result) {
                item.PicturesLinkParts = (await _dataRepo.GetProductPictureLinkPartsAsync(item.ProductId, "medium", "site"))
                    .Select(x => new PictureLinkPart {
                        LinkPart = x.LinkPart.Trim(),
                        OrderNum = x.OrderNum,
                        PicSize = x.PicSize
                    }).OrderBy(x => x.OrderNum)
                    .ToList();

                item.ProductPictureLinkPart = item.PicturesLinkParts.FirstOrDefault()?.LinkPart.Trim() ?? string.Empty;
            }

            return result;
        }
    }
}
