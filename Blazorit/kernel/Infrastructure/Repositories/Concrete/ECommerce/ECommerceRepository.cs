﻿using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Microsoft.Extensions.Logging;

namespace Blazorit.Infrastructure.Repositories.Concrete.ECommerce {
    public class ECommerceRepository : IECommerceRepository {
        private readonly IDbContextFactory<BlazoritContext> _contextFactory;
        private readonly ILogger? _logger;

        //public ECommerceRepository(IDbContextFactory<BlazoritContext> contextFactory) {
        //    _contextFactory = contextFactory;
        //}


        public ECommerceRepository(IDbContextFactory<BlazoritContext> contextFactory, ILogger<ECommerceRepository> logger) {
            _contextFactory = contextFactory;
            _logger = logger;
        }



        public async Task<(bool ok, string sku)> AddProductAsync(string name, string curr, decimal price, string? description, string? categoryName) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdCategory? category = null;
                string prefixSku = string.Empty;

                ProdProduct product = new ProdProduct {
                    Name = name,
                    Curr = curr,
                    Price = price,
                    Description = description
                };

                if (categoryName != null) {
                    category = await context.ProdCategories.Where(x => x.Name == categoryName).FirstOrDefaultAsync();
                }

                if (category != null && category.PrefixSku != null) {
                    prefixSku = $"{category.PrefixSku}-";
                }

                product.Category = category;
                long maxProductId = context.ProdProducts.Max(x => x.Id);
                product.Sku = prefixSku + (1200 + (maxProductId + 1)).ToString(); //auto SKU (you can use any logic for auto SKU)

                await context.ProdProducts.AddAsync(product);
                await context.SaveChangesAsync();
                return (true, product.Sku);
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, string.Empty);
        }


        public async Task<(bool ok, string sku)> AddProductAsync(string sku, string name, string curr, decimal price, string? description, string? categoryName) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdCategory? category = null;

                ProdProduct product = new ProdProduct {
                    Name = name,
                    Curr = curr,
                    Price = price,
                    Description = description
                };

                if (categoryName != null) {
                    category = await context.ProdCategories.Where(x => x.Name == categoryName).FirstOrDefaultAsync();
                }

                product.Category = category;
                product.Sku = sku;

                await context.ProdProducts.AddAsync(product);
                await context.SaveChangesAsync();
                return (true, product.Sku);
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, string.Empty);
        }


        public async Task<(bool ok, long cartId)> AddProductToCartAsync(long userId, string productSKU, int quantity) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

                if (product == null) {
                    return (false, 0);
                }

                CartShopcart? cart = await context.CartShopcarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                if (cart is null) {
                    cart = new CartShopcart { UserId = userId };
                    await context.CartShopcarts.AddAsync(cart);
                }

                CartShopcartList cartList = new CartShopcartList() {
                    Cart = cart,
                    Product = product,
                    Quantity = quantity
                };

                await context.CartShopcartLists.AddAsync(cartList);
                await context.SaveChangesAsync();
                return (true, cart.Id);
            
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToCartAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, 0);
        }


        public async Task<bool> AddProductToCartByCartIdAsync(long cartId, string productSKU, int quantity) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

                if (product == null) {
                    return false;
                }

                CartShopcartList cartList = new CartShopcartList() {
                    CartId = cartId,
                    Product = product,
                    Quantity = quantity
                };

                await context.CartShopcartLists.AddAsync(cartList);
                await context.SaveChangesAsync();
                return true;

            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToCartByCartIdAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return false;
        }


        /// <summary>
        /// Method adds product to user's wishlist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <returns>(Success, wishlistId)</returns>
        public async Task<(bool ok, long wishId)> AddProductToWishlistAsync(long userId, string productSKU) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

                if (product == null) {
                    return (false, 0);
                }

                WishWish? wish = await context.WishWishes.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                if (wish is null) {
                    wish = new WishWish { UserId = userId };
                    await context.WishWishes.AddAsync(wish);
                }

                WishWishList wishList = new WishWishList() {
                    Wish = wish,
                    Product = product
                };

                await context.WishWishLists.AddAsync(wishList);
                await context.SaveChangesAsync();
                return (true, wish.Id);

            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToWishlistAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, 0);
        }


        /// <summary>
        /// Method adds product to user's wishlist (this method is a bit faster than AddProductToWishlistAsync method)
        /// </summary>
        /// <param name="wishlistId"></param>
        /// <param name="productSKU"></param>
        /// <returns></returns>
        public async Task<bool> AddProductToWishlistByWishlistIdAsync(long wishlistId, string productSKU) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

                if (product == null) {
                    return false;
                }                

                WishWishList wishList = new WishWishList() {
                    WishId = wishlistId,
                    Product = product
                };

                await context.WishWishLists.AddAsync(wishList);
                await context.SaveChangesAsync();
                return true;

            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToWishlistByWishlistIdAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return false;
        }
    }
}
