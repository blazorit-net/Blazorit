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
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders;

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
        /// Method adds product to user's cart by SKU
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

                if (cart is null) { //create cart and insert new product for the cart
                    cart = new CartShopcart { UserId = userId };
                    await context.CartShopcarts.AddAsync(cart);

                    CartShopcartList cartList = new() {
                        Cart = cart,
                        Product = product,
                        Quantity = quantity
                    };

                    await context.CartShopcartLists.AddAsync(cartList);
                } else {
                    CartShopcartList? cartList = await context.CartShopcartLists.Where(x => x.CartId == cart.Id && x.ProductId == product.Id).FirstOrDefaultAsync();

                    //insert new product for the cart
                    if (cartList is null) {
                        cartList = new CartShopcartList() {
                            Cart = cart,
                            Product = product,
                            Quantity = quantity
                        };

                        await context.CartShopcartLists.AddAsync(cartList);
                    } else { //update quantity
                        // check cart item for logic (zero or negative number) quantity 
                        if ((cartList.Quantity + quantity) > 0)
                        {
                            cartList.Quantity += quantity;
                        } else
                        {
                            return (false, 0);
                        }                
                    }
                }       
               
                await context.SaveChangesAsync();
                return (true, cart.Id);
            
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToCartAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, 0);
        }


        /// <summary>
        /// Method adds product to user's cart by productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<(bool ok, long cartId)> AddProductToCartAsync(long userId, long productId, int quantity)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                CartShopcart? cart = await context.CartShopcarts.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                if (cart is null)
                { //create cart and insert new product for the cart
                    cart = new CartShopcart { UserId = userId };
                    await context.CartShopcarts.AddAsync(cart);

                    CartShopcartList cartList = new()
                    {
                        Cart = cart,
                        ProductId = productId,
                        Quantity = quantity
                    };

                    await context.CartShopcartLists.AddAsync(cartList);
                }
                else
                {
                    CartShopcartList? cartList = await context.CartShopcartLists.Where(x => x.CartId == cart.Id && x.ProductId == productId).FirstOrDefaultAsync();

                    //insert new product for the cart
                    if (cartList is null)
                    {
                        cartList = new CartShopcartList()
                        {
                            Cart = cart,
                            ProductId = productId,
                            Quantity = quantity
                        };

                        await context.CartShopcartLists.AddAsync(cartList);
                    }
                    else
                    { //update quantity
                      // check cart item for logic (zero or negative number) quantity 
                        if ((cartList.Quantity + quantity) > 0)
                        {
                            cartList.Quantity += quantity;
                        }
                        else
                        {
                            return (false, 0);
                        }
                    }
                }

                await context.SaveChangesAsync();
                return (true, cart.Id);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToCartAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, 0);
        }


        /////// <summary>
        /////// Method adds product to user's cart by cartId (this method is a bit faster than AddProductToCartAsync method)
        /////// </summary>
        /////// <param name="cartId"></param>
        /////// <param name="productSKU"></param>
        /////// <param name="quantity"></param>
        /////// <returns>Success</returns>
        ////public async Task<bool> AddProductToCartByCartIdAsync(long cartId, string productSKU, int quantity) {
        ////    try {
        ////        using var context = await _contextFactory.CreateDbContextAsync();

        ////        ProdProduct? product = await context.ProdProducts.Where(prod => prod.Sku == productSKU).FirstOrDefaultAsync();

        ////        if (product == null) {
        ////            return false;
        ////        }

        ////        CartShopcartList cartList = new() {
        ////            CartId = cartId,
        ////            Product = product,
        ////            Quantity = quantity
        ////        };

        ////        await context.CartShopcartLists.AddAsync(cartList);
        ////        await context.SaveChangesAsync();
        ////        return true;

        ////    } catch (Exception ex) {
        ////        _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductToCartByCartIdAsync)} of the {nameof(ECommerceRepository)} repository");
        ////    }

        ////    return false;
        ////}


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
        /// Method creates payment info in DB
        /// </summary>
        /// <param name="paymentAmount"></param>
        /// <param name="manyParamsAboutPayments">you can extend your table for other fields, and this param must be deleted, and insert other params to method signature</param>
        /// <returns></returns>
        public async Task<(bool ok, long paymentId)> CreatePaymentInfoAsync(decimal paymentAmount, string? manyParamsAboutPayments = null)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                PmntPayment payment = new() 
                { 
                    PaymentAmount = paymentAmount                 
                };

                await context.PmntPayments.AddAsync(payment);
                await context.SaveChangesAsync();
                return (true, payment.Id);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(CreatePaymentInfoAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, default);
        }


        /// <summary>
        /// Method create or returns exists id of user delivery point
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="addressId"></param>
        /// <returns>user delivery point ID</returns>
        public async Task<(bool ok, long deliveryId)> InitUserDeliveryAsync(long userId, long methodId, long addressId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                DlyUserDelivery? userDelivery = await context.DlyUserDeliveries
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.MethodId == methodId && x.AddressId == addressId);
            
                if (userDelivery == null)
                {
                    userDelivery = new DlyUserDelivery
                    {
                        UserId = userId,
                        MethodId = methodId,
                        AddressId = addressId
                    };

                    await context.DlyUserDeliveries.AddAsync(userDelivery);
                    await context.SaveChangesAsync();
                }

                return (true, userDelivery.Id);
            
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(CreatePaymentInfoAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, default);
        }


        /// <summary>
        /// Method create order from cart for User by userId
        /// And this method removes all items from user's shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryId) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                List<VwCartShopcart>? cartList = context.VwCartShopcarts.Where(x => x.UserId == userId).ToList();
                
                if (cartList == null) {
                    return false;
                }

                OrdOrder order = new() {
                    UserId = userId,
                    PaymentId = paymentId,
                    DeliveryId = deliveryId
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

                // remove all items from user's shopcart
                context.RemoveRange(context.CartShopcartLists.Where(x => x.Cart.UserId == userId));
                context.RemoveRange(context.CartShopcarts.Where(x => x.UserId == userId));

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


        /// <summary>
        /// Method returns picture's link parts of one product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pic_size"></param>
        /// <param name="site_location"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Method returns all user's items from shop cart 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VwShopcart>> GetShopCartListAsync(long userId) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                var list = await context.VwCartShopcarts
                    .Where(x => x.UserId == userId)
                    .Select(x => new VwShopcart {
                        CartId = x.CartId.GetValueOrDefault(),
                        Curr = x.Curr!,
                        DateTimeCreated = x.DateTimeCreated.GetValueOrDefault(),
                        Name = x.Name!, 
                        ProductId = x.ProductId.GetValueOrDefault(),
                        ProductPrice = x.ProductPrice.GetValueOrDefault(),
                        Quantity = x.Quantity.GetValueOrDefault(),
                        Sku = x.Sku!,
                        ProductLinkPart = x.ProductLinkPart!,
                        Category = x.Category!                        
                    })
                    .ToListAsync();

                return list;
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetShopCartListAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return new List<VwShopcart>();
        }


        /// <summary>
        /// Method merges (sourceCart) shopcart with storage cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sourceCart"></param>
        /// <returns>Result cart</returns>
        public async Task<IEnumerable<VwShopcart>> UpdateShopCartAsync(long userId, IEnumerable<VwShopcart> sourceCart) {
            try {
                using var context = await _contextFactory.CreateDbContextAsync();

                CartShopcart? repoCart = context.CartShopcarts.FirstOrDefault(x => x.UserId == userId);
                
                if (repoCart is null) {
                    repoCart = new CartShopcart { UserId = userId };
                    await context.CartShopcarts.AddAsync(repoCart);
                }

                foreach(var item in sourceCart) {
                    CartShopcartList? row = context.CartShopcartLists.FirstOrDefault(x => x.CartId == repoCart.Id && x.ProductId == item.ProductId);
                    if (row is not null) {
                        row.Quantity = item.Quantity;
                        //row.DateTimeModified = DateTime.Now;
                    } else {
                        row = new CartShopcartList {
                            Cart = repoCart,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                            ////DateTimeCreated = item.DateTimeCreated.UtcDateTime
                        };
                        await context.CartShopcartLists.AddAsync(row);
                    }                    
                }

                await context.SaveChangesAsync();              
            } catch (Exception ex) {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(UpdateShopCartAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return await GetShopCartListAsync(userId);
        }


        /// <summary>
        /// Method returns all delivery methods
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.DlyDeliveryMethods.Select(x => new DeliveryMethod
                {
                     Id = x.Id,
                     Method = x.Method,
                     EnterAddress = x.EnterAddress
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetDeliveryMethodsAsync)} of the {nameof(ECommerceRepository)} repository");               
            }

            return new List<DeliveryMethod>();
        }


        /// <summary>
        /// Method returns DeliveryAddresses for choosen delivery method for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddressesAsync(long userId, long methodId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.DlyUserDeliveries
                    .Where(x => x.UserId == userId && x.MethodId == methodId)
                    .Select(x => x.Address)
                    .Select(x => new DeliveryAddress
                    {
                        Address = x.Address,
                        Id = x.Id,
                        Comment = x.Comment ?? string.Empty
                    })
                    .ToListAsync();                    
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetDeliveryAddressesAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return new List<DeliveryAddress>();
        }

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(long userId, long methodId, string address)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                DlyDeliveryAddress newAddress = new()
                {
                    Address = address
                };

                await context.DlyDeliveryAddresses.AddAsync(newAddress);
                await context.DlyUserDeliveries
                    .AddAsync(new DlyUserDelivery
                    {
                        UserId = userId,
                        Address = newAddress,
                        MethodId = methodId
                    });

                await context.SaveChangesAsync();
                
                return await context.DlyUserDeliveries
                    .Where(x => x.UserId == userId && x.MethodId == methodId)
                    .Select(x => x.Address)
                    .Select(x => new DeliveryAddress
                    {
                        Address = x.Address,
                        Id = x.Id,
                        Comment = x.Comment ?? string.Empty,
                        DateTimeCreated = x.DateTimeCreated
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddDeliveryAddressAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return new List<DeliveryAddress>();
        }


        /// <summary>
        /// Method returns common DeliveryAddresses for choosen delivery method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetCommonDeliveryAddressesAsync(long methodId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.DlyMethodsAddresses
                    .Where(x => x.MethodId == methodId)
                    .Select(x => x.Address)
                    .Select(x => new DeliveryAddress
                    {
                        Address = x.Address,
                        Id = x.Id,
                        Comment = x.Comment ?? string.Empty
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetCommonDeliveryAddressesAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return new List<DeliveryAddress>();
        }


        /// <summary>
        /// Method returns User delivery (Id)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task<UserDelivery?> GetUserDelivery (long userId, long methodId, long addressId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.DlyUserDeliveries
                    .Where(x => x.UserId == userId && x.MethodId == methodId && x.AddressId == addressId)
                    .Select(x => new UserDelivery
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        MethodId = x.MethodId,
                        AddressId = x.AddressId,
                        DateTimeCreated = x.DateTimeCreated
                    })
                    .FirstOrDefaultAsync();                    
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetUserDelivery)} of the {nameof(ECommerceRepository)} repository");
            }

            return null;
        }

        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="paymentToken"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="deliveryAddressId"></param>
        /// <returns></returns>
        public async Task<bool> CreateUniqOrderTokenAsync(string orderToken, decimal paymentAmount, long userId, long deliveryMethodId, long deliveryAddressId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                await context.OrdCheckoutOrders.AddAsync(
                    new OrdCheckoutOrder
                    {
                        OrderToken = orderToken,
                        Canceled = false,
                        PaymentAmount = paymentAmount,    
                        UserId = userId,
                        DeliveryMethodId = deliveryMethodId,
                        DeliveryAddressId = deliveryAddressId
                    });

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(CreateUniqOrderTokenAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Methods returns info about order by paymentToken
        /// </summary>
        /// <param name="paymentToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CheckoutOrder?> GetTokenOrderInfoAsync(string orderToken, long userId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                CheckoutOrder? checkoutOrder = await context.OrdCheckoutOrders
                    .Where(x => x.OrderToken == orderToken && x.UserId == userId)
                    .Select(x => new CheckoutOrder
                    {
                        OrderToken = x.OrderToken,
                        Canceled = x.Canceled ?? default,
                        PaymentAmount = x.PaymentAmount,
                        UserId = x.UserId,
                        DeliveryMethodId = x.DeliveryMethodId,
                        DeliveryAddressId = x.DeliveryAddressId,
                        DateTimeCreated = x.DateTimeCreated
                    })
                    .FirstOrDefaultAsync();

                return checkoutOrder;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetTokenOrderInfoAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return null;
        }
    }
}
