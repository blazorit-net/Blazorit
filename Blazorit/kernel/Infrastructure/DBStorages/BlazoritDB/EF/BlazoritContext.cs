﻿using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.ident;
using Microsoft.EntityFrameworkCore;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF {
    public partial class BlazoritContext : DbContext {
        public BlazoritContext(DbContextOptions<BlazoritContext> options) : base(options) {
        }

        //################################################################
        //  #######################--IDENT--############################
        //################################################################
        public virtual DbSet<User> Users { get; set; } = null!;
        //################################################################
        //  ############################################################
        //################################################################


        //################################################################
        //  ###################--DOM ECOMMERCE--########################
        //################################################################
        public virtual DbSet<CartShopcart> CartShopcarts { get; set; }

        public virtual DbSet<CartShopcartList> CartShopcartLists { get; set; }

        public virtual DbSet<DlyDelivery> DlyDeliveries { get; set; }

        public virtual DbSet<DlyDeliveryAddress> DlyDeliveryAddresses { get; set; }

        public virtual DbSet<DlyDeliveryMethod> DlyDeliveryMethods { get; set; }

        public virtual DbSet<DlyMethodsAddress> DlyMethodsAddresses { get; set; }

        public virtual DbSet<DlyUserDelivery> DlyUserDeliveries { get; set; }

        public virtual DbSet<OrdCheckoutOrder> OrdCheckoutOrders { get; set; }

        public virtual DbSet<OrdOrder> OrdOrders { get; set; }

        public virtual DbSet<OrdOrderList> OrdOrderLists { get; set; }

        public virtual DbSet<PmntPayment> PmntPayments { get; set; }

        public virtual DbSet<PmntPaymentMethod> PmntPaymentMethods { get; set; }

        public virtual DbSet<ProdCategory> ProdCategories { get; set; }

        public virtual DbSet<ProdPicture> ProdPictures { get; set; }

        public virtual DbSet<ProdProduct> ProdProducts { get; set; }

        public virtual DbSet<VwCartShopcart> VwCartShopcarts { get; set; }

        public virtual DbSet<VwDlyDelivery> VwDlyDeliveries { get; set; }

        public virtual DbSet<VwDlyMethodsAddress> VwDlyMethodsAddresses { get; set; }

        public virtual DbSet<VwDlyUserDelivery> VwDlyUserDeliveries { get; set; }

        public virtual DbSet<VwOrdOrder> VwOrdOrders { get; set; }

        public virtual DbSet<VwProdProduct> VwProdProducts { get; set; }

        public virtual DbSet<WishWish> WishWishes { get; set; }

        public virtual DbSet<WishWishList> WishWishLists { get; set; }

        //################################################################
        //  ############################################################
        //################################################################


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#############################################################
            //  #######################--IDENT--#########################
            //#############################################################
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users", "ident");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DateCreated).HasColumnName("date_created");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");
                entity.Property(e => e.UserRole)
                    .HasMaxLength(100)
                    .HasColumnName("user_role");
            });
            //################################################################
            //  ############################################################
            //################################################################


            //################################################################
            //  ###################--DOM ECOMMERCE--########################
            //################################################################
            modelBuilder.Entity<CartShopcart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("cart_shopcarts_pkey");

                entity.ToTable("cart_shopcarts", "dom");

                entity.HasIndex(e => e.UserId, "UQIX__cart_shopcarts__user_id").IsUnique();

                entity.HasIndex(e => e.UserId, "fki_fk__cart_shop_carts__ident_users");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<CartShopcartList>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("cart_shopcart_lists_pkey");

                entity.ToTable("cart_shopcart_lists", "dom");

                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.DateTimeCreated)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_created");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Cart).WithMany(p => p.CartShopcartLists)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__cart_shopcart_lists__cart_shopcart");

                entity.HasOne(d => d.Product).WithMany(p => p.CartShopcartLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__cart_shopcart_lists__prod_products");
            });

            modelBuilder.Entity<DlyDelivery>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dly_deliveries_pkey");

                entity.ToTable("dly_deliveries", "dom");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.DeliveryCost)
                    .HasPrecision(16, 4)
                    .HasColumnName("delivery_cost");
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
                entity.Property(e => e.DeliveryTimeEnd)
                    .HasColumnType("time with time zone")
                    .HasColumnName("delivery_time_end");
                entity.Property(e => e.DeliveryTimeStart)
                    .HasColumnType("time with time zone")
                    .HasColumnName("delivery_time_start");
                entity.Property(e => e.UserDeliveryId).HasColumnName("user_delivery_id");

                entity.HasOne(d => d.UserDelivery).WithMany(p => p.DlyDeliveries)
                    .HasForeignKey(d => d.UserDeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fki_fk__dly_deliveries__dly_user_deliveries");
            });

            modelBuilder.Entity<DlyDeliveryAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dly_delivery_address_pkey");

                entity.ToTable("dly_delivery_addresses", "dom");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");
                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .HasColumnName("comment");
                entity.Property(e => e.DateTimeCreated)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_created");
            });

            modelBuilder.Entity<DlyDeliveryMethod>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dly_delivery_methods_pkey");

                entity.ToTable("dly_delivery_methods", "dom");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.EnterAddress).HasColumnName("enter_address");
                entity.Property(e => e.Method)
                    .HasMaxLength(256)
                    .HasColumnName("method");
            });

            modelBuilder.Entity<DlyMethodsAddress>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dly_methods_addresses_pkey");

                entity.ToTable("dly_methods_addresses", "dom", tb => tb.HasComment("The table is for shipping methods where the address offered by the system is common to all users"));

                entity.HasIndex(e => new { e.MethodId, e.AddressId }, "UK__dly_methods_addresses").IsUnique();

                entity.HasIndex(e => e.AddressId, "fki_FK__dly_methods_addresses__dly_delivery_addresses");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.MethodId).HasColumnName("method_id");

                entity.HasOne(d => d.Address).WithMany(p => p.DlyMethodsAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dly_methods_addresses__dly_delivery_addresses");

                entity.HasOne(d => d.Method).WithMany(p => p.DlyMethodsAddresses)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dly_methods_addresses__dly_delivery_methods");
            });

            modelBuilder.Entity<DlyUserDelivery>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("dly_user_deliveries_pkey");

                entity.ToTable("dly_user_deliveries", "dom", tb => tb.HasComment("Addresses entered by the users"));

                entity.HasIndex(e => new { e.UserId, e.MethodId, e.AddressId }, "UQ__dly_user_deliveries").IsUnique();

                entity.HasIndex(e => e.AddressId, "fki_FK__dly_user_deliveries__dly_delivery_addresses");

                entity.HasIndex(e => e.MethodId, "fki_FK__dly_user_deliveries__dly_delivery_methods");

                entity.HasIndex(e => e.UserId, "fki_FK__dly_user_deliveries__ident_users");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.DateTimeCreated)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_created");
                entity.Property(e => e.MethodId).HasColumnName("method_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Address).WithMany(p => p.DlyUserDeliveries)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dly_user_deliveries__dly_delivery_addresses");

                entity.HasOne(d => d.Method).WithMany(p => p.DlyUserDeliveries)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dly_user_deliveries__dly_delivery_methods");
            });

            modelBuilder.Entity<OrdCheckoutOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ord_checkout_orders_pkey");

                entity.ToTable("ord_checkout_orders", "dom", tb => tb.HasComment("this table need for temporary storage info about order, while payment is being made"));

                entity.HasIndex(e => e.OrderToken, "UQ__ord_checkout_orders__order_token").IsUnique();

                entity.HasIndex(e => new { e.OrderToken, e.Canceled }, "uqix__ord_checkout_orders__order_token__canceled").IsUnique();

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.Canceled)
                    .IsRequired()
                    .HasDefaultValueSql("true")
                    .HasComment("if this field is canceled, than you can delete this row from table.")
                    .HasColumnName("canceled");
                entity.Property(e => e.DateTimeCreated)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_created");
                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
                entity.Property(e => e.OrderToken)
                    .HasMaxLength(100)
                    .HasComment("uniq token")
                    .HasColumnName("order_token");
                entity.Property(e => e.PaymentAmount)
                    .HasPrecision(16, 4)
                    .HasColumnName("payment_amount");
                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<OrdOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ord_orders_pkey");

                entity.ToTable("ord_orders", "dom");

                entity.HasIndex(e => e.DeliveryId, "UQ__ord_orders__delivery_id").IsUnique();

                entity.HasIndex(e => e.PaymentId, "UQ__ord_orders__payment_id").IsUnique();

                entity.HasIndex(e => e.DeliveryId, "fki_fk__ord_orders__dly_deliveries");

                entity.HasIndex(e => e.DeliveryId, "fki_fk__ord_orders__dly_user_deliveries");

                entity.HasIndex(e => e.PaymentId, "fki_fk__ord_orders__pmnt_payments");

                entity.HasIndex(e => e.UserId, "fki_fk__ord_orders__users__id");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Delivery).WithOne(p => p.OrdOrder)
                    .HasForeignKey<OrdOrder>(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__ord_orders__dly_deliveries");

                entity.HasOne(d => d.Payment).WithOne(p => p.OrdOrder)
                    .HasForeignKey<OrdOrder>(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__ord_orders__pmnt_payments");
            });

            modelBuilder.Entity<OrdOrderList>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("ord_order_list_pkey");

                entity.ToTable("ord_order_lists", "dom");

                entity.HasIndex(e => e.OrderId, "fki_fk__ord_order_list__ord_order");

                entity.HasIndex(e => e.ProductId, "fki_fk__ord_order_list__prod_products");

                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Price)
                    .HasPrecision(16, 4)
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order).WithMany(p => p.OrdOrderLists)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__ord_order_list__ord_order");

                entity.HasOne(d => d.Product).WithMany(p => p.OrdOrderLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__ord_order_list__prod_products");
            });

            modelBuilder.Entity<PmntPayment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pmnt_payments_pkey");

                entity.ToTable("pmnt_payments", "dom");

                entity.HasIndex(e => e.CheckoutOrderId, "UQ__checkout_order_id").IsUnique();

                entity.HasIndex(e => e.PaymentMethodId, "fki_fk__pmnt_payments__pmnt_payment_methods");

                entity.HasIndex(e => e.CheckoutOrderId, "fki_fk_not_valid__pmnt_payments__checout_orders__id");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.CheckoutOrderId)
                    .HasComment("this field relation to id of ord_checkout_orders, but rows in ord_checkout_orders table can be deleted (whose canceled), than we have not Foreign key to ord_checkout_orders")
                    .HasColumnName("checkout_order_id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.IsPaid).HasColumnName("is_paid");
                entity.Property(e => e.OrderToken)
                    .HasMaxLength(100)
                    .HasColumnName("order_token");
                entity.Property(e => e.PaymentAmount)
                    .HasPrecision(16, 4)
                    .HasColumnName("payment_amount");
                entity.Property(e => e.PaymentInfo).HasColumnName("payment_info");
                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PmntPayments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__pmnt_payments__pmnt_payment_methods");
            });

            modelBuilder.Entity<PmntPaymentMethod>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pmnt_payment_methods_pkey");

                entity.ToTable("pmnt_payment_methods", "dom");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.IsCod)
                    .HasComment("Is Cash On Delivery")
                    .HasColumnName("is_cod");
                entity.Property(e => e.Method)
                    .HasMaxLength(200)
                    .HasColumnName("method");
                entity.Property(e => e.Ordby).HasColumnName("ordby");
            });

            modelBuilder.Entity<ProdCategory>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("prod_categories_pkey");

                entity.ToTable("prod_categories", "dom");

                entity.HasIndex(e => e.Name, "UQIX__prod_categories__name").IsUnique();

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, null, null, null)
                    .HasColumnName("id");
                entity.Property(e => e.FullName)
                    .HasMaxLength(200)
                    .HasColumnName("full_name");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.PrefixSku)
                    .HasMaxLength(20)
                    .HasColumnName("prefix_sku");
            });

            modelBuilder.Entity<ProdPicture>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("prod_pictures_pkey");

                entity.ToTable("prod_pictures", "dom");

                entity.HasIndex(e => new { e.ProductId, e.PicSize, e.SiteLocation }, "IX__prod_pictures__include");

                entity.HasIndex(e => new { e.ProductId, e.PicSize, e.SiteLocation, e.OrderNum }, "UQ__prod_pictures").IsUnique();

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.LinkPart)
                    .HasMaxLength(100)
                    .HasColumnName("link_part");
                entity.Property(e => e.OrderNum).HasColumnName("order_num");
                entity.Property(e => e.PicSize)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'medium'::character varying")
                    .HasColumnName("pic_size");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.SiteLocation)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'site'::character varying")
                    .HasColumnName("site_location");

                entity.HasOne(d => d.Product).WithMany(p => p.ProdPictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__prod_pictures__prod_products");
            });

            modelBuilder.Entity<ProdProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("prod_products_pkey");

                entity.ToTable("prod_products", "dom");

                entity.HasIndex(e => new { e.LinkPart, e.CategoryId }, "UQIX__prod_products__include").IsUnique();

                entity.HasIndex(e => e.Sku, "UQIX__prod_products__sku").IsUnique();

                entity.HasIndex(e => e.CategoryId, "fki_fk__prod_products__prod_categories");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.DateTimeModified)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_modified");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.IsOnSite)
                    .IsRequired()
                    .HasDefaultValueSql("true")
                    .HasComment("if on_site is true than this product show on the site, else not show")
                    .HasColumnName("is_on_site");
                entity.Property(e => e.LinkPart)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("'empty'::character varying")
                    .HasColumnName("link_part");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(16, 4)
                    .HasColumnName("price");
                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasComment("articul")
                    .HasColumnName("sku");

                entity.HasOne(d => d.Category).WithMany(p => p.ProdProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__prod_products__prod_categories");
            });

            modelBuilder.Entity<VwCartShopcart>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_cart_shopcarts", "dom");

                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.DateTimeItemCreate).HasColumnName("date_time_item_create");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductLinkPart)
                    .HasMaxLength(200)
                    .HasColumnName("product_link_part");
                entity.Property(e => e.ProductPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("product_price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("sku");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VwDlyDelivery>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_dly_deliveries", "dom");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .HasColumnName("comment");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.DeliveryCost)
                    .HasPrecision(16, 4)
                    .HasColumnName("delivery_cost");
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
                entity.Property(e => e.DeliveryTimeEnd)
                    .HasColumnType("time with time zone")
                    .HasColumnName("delivery_time_end");
                entity.Property(e => e.DeliveryTimeStart)
                    .HasColumnType("time with time zone")
                    .HasColumnName("delivery_time_start");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Method)
                    .HasMaxLength(256)
                    .HasColumnName("method");
                entity.Property(e => e.MethodId).HasColumnName("method_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VwDlyMethodsAddress>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_dly_methods_addresses", "dom");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .HasColumnName("comment");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Method)
                    .HasMaxLength(256)
                    .HasColumnName("method");
                entity.Property(e => e.MethodId).HasColumnName("method_id");
            });

            modelBuilder.Entity<VwDlyUserDelivery>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_dly_user_deliveries", "dom");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");
                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .HasColumnName("comment");
                entity.Property(e => e.DateTimeCreated).HasColumnName("date_time_created");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Method)
                    .HasMaxLength(256)
                    .HasColumnName("method");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VwOrdOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_ord_orders", "dom");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.OrderPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("order_price");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductLinkPart)
                    .HasMaxLength(200)
                    .HasColumnName("product_link_part");
                entity.Property(e => e.ProductPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("product_price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("sku");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VwProdProduct>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_prod_products", "dom");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .HasColumnName("category");
                entity.Property(e => e.CategoryFullName)
                    .HasMaxLength(200)
                    .HasColumnName("category_full_name");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateModified).HasColumnName("date_modified");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.DateTimeModified).HasColumnName("date_time_modified");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IsOnSite).HasColumnName("is_on_site");
                entity.Property(e => e.LinkPart)
                    .HasMaxLength(200)
                    .HasColumnName("link_part");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(16, 4)
                    .HasColumnName("price");
                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("sku");
            });

            modelBuilder.Entity<WishWish>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("wish_wishes_pkey");

                entity.ToTable("wish_wishes", "dom");

                entity.HasIndex(e => e.UserId, "UQIX__wish_wishes__user_id").IsUnique();

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<WishWishList>(entity =>
            {
                entity.HasKey(e => new { e.WishId, e.ProductId }).HasName("wish_wish_lists_pkey");

                entity.ToTable("wish_wish_lists", "dom");

                entity.Property(e => e.WishId).HasColumnName("wish_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");

                entity.HasOne(d => d.Product).WithMany(p => p.WishWishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__wish_wish_lists__prod_products");

                entity.HasOne(d => d.Wish).WithMany(p => p.WishWishLists)
                    .HasForeignKey(d => d.WishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__wish_wish_lists__wish_wishes");
            });
            //################################################################
            //  ############################################################
            //################################################################

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}