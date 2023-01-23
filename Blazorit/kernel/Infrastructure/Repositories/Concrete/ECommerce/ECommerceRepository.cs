using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Microsoft.Extensions.Logging;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System.Collections;

namespace Blazorit.Infrastructure.Repositories.Concrete.ECommerce
{
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


        /// <summary>
        /// Method adds product to products repository. This method assigns unique SKU for the product
        /// </summary>
        /// <param name="name">Product name</param>
        /// <param name="curr">Currency</param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <returns>(Success, unique SKU)</returns>
        public async Task<(bool ok, string sku)> AddProductAsync(string name, string curr, decimal price, string? description, string? categoryName) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdCategory? category = null;
                string prefixSku = string.Empty;

                ProdProduct product = new() {
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


        /// <summary>
        /// Method adds product to products repository. You need assign SKU for the product
        /// </summary>
        /// <param name="sku">SKU (Stock Keeping Unit) (articul)</param>
        /// <param name="name">Product name</param>
        /// <param name="curr">Currency</param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <returns>(Success, unique SKU)</returns>
        public async Task<(bool ok, string sku)> AddProductAsync(string sku, string name, string curr, decimal price, string? description, string? categoryName) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdCategory category = null!;

                ProdProduct product = new() {
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


        /// <summary>
        /// Method adds product to user's cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>(Success, cartId)</returns>
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

                CartShopcartList cartList = new() {
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


        /// <summary>
        /// Method adds product to user's cart by cartId (this method is a bit faster than AddProductToCartAsync method)
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>Success</returns>
        public async Task<bool> AddProductToCartByCartIdAsync(long cartId, string productSKU, int quantity) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

                if (product == null) {
                    return false;
                }

                CartShopcartList cartList = new() {
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

                WishWishList wishList = new() {
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

                WishWishList wishList = new() {
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


        /// <summary>
        /// Method create order from cart for User by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrderFromCart(long userId) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                List<VwCartShopcart>? cartList = context.VwCartShopcarts.Where(x => x.UserId == userId).ToList();
                
                if (cartList == null) {
                    return false;
                }

                OrdOrder order = new() {
                    UserId = userId
                };              

                foreach (var item in cartList) {
                    OrdOrderList orderListItem = new() {
                        Order = order,
                        ProductId = item.ProductId ?? 0,
                        //Product = item.Product,
                        Price = item.ProductPrice,
                        Quantity = item.Quantity                        
                    };

                    await context.OrdOrderLists.AddAsync(orderListItem);
                }

                await context.OrdOrders.AddAsync(order); 
                await context.SaveChangesAsync();
                return true;

            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(CreateOrderFromCart)} of the {nameof(ECommerceRepository)} repository");
            }

            return false;
        }


        /// <summary>
        /// Method returns all products from product's view
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VwProduct>> GetProducts() {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                return context.VwProdProducts ////.ToList()
                    .Select(x => new VwProduct {
                            Category = x.Category!,
                            CategoryFullName = x.CategoryFullName!,
                            Curr = x.Curr!,
                            DateCreate = x.DateCreate.GetValueOrDefault(),
                            DateModified = x.DateModified.GetValueOrDefault(),
                            DateTimeCreate = x.DateTimeCreate.GetValueOrDefault(),
                            DateTimeModified = x.DateTimeModified.GetValueOrDefault(),
                            Id = x.Id.GetValueOrDefault(),
                            Name= x.Name!,
                            Price = x.Price.GetValueOrDefault(),
                            Sku = x.Sku!,
                            LinkPart = x.LinkPart!
                    })
                    .ToList();
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetProducts)} of the {nameof(ECommerceRepository)} repository");
            }

            return new List<VwProduct>();
        }


        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        public async Task<VwProduct?> GetProductDataAsync(string category, string linkPart) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                var product = context.VwProdProducts
                    .Where(x => x.Category == category && x.LinkPart == linkPart)
                    .Select(x => new VwProduct {
                        Category = x.Category!,
                        CategoryFullName = x.CategoryFullName!,
                        Curr = x.Curr!,
                        DateCreate = x.DateCreate.GetValueOrDefault(),
                        DateModified = x.DateModified.GetValueOrDefault(),
                        DateTimeCreate = x.DateTimeCreate.GetValueOrDefault(),
                        DateTimeModified = x.DateTimeModified.GetValueOrDefault(),
                        Description = x.Description,
                        Id = x.Id.GetValueOrDefault(),
                        Name = x.Name!,
                        Price = x.Price.GetValueOrDefault(),
                        Sku = x.Sku!,
                        LinkPart = x.LinkPart!
                    })
                    .FirstOrDefault();

                //if (product is not null) {
                //    product.Description = context.ProdProducts.Where(x => x.Id == product.Id).FirstOrDefault().Description!;
                //}
                
                return product;

            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetProductDataAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return null;
        }


        public async Task<IEnumerable<PictureLinkPart>> GetProductPictureLinkPartsAsync(long productId, string pic_size, string site_location) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                var linkParts = await context.ProdPictures
                    .Where(x => x.ProductId == productId && x.PicSize == pic_size && x.SiteLocation == site_location)
                    .Select(x => new PictureLinkPart {
                        LinkPart = x.LinkPart,
                        OrderNum = x.OrderNum,
                        PicSize = x.PicSize
                    })
                    .ToListAsync();

                return linkParts;

            } catch (Exception ex) { 
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetProductPictureLinkPartsAsync)} of the {nameof(ECommerceRepository)} repository");
            }
            
            return new List<PictureLinkPart>();
        }
    }
}
