using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
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

        public virtual DbSet<OrdOrder> OrdOrders { get; set; }

        public virtual DbSet<OrdOrderList> OrdOrderLists { get; set; }

        public virtual DbSet<ProdProduct> ProdProducts { get; set; }

        public virtual DbSet<VwCartShopcart> VwCartShopcarts { get; set; }

        public virtual DbSet<VwOrdOrder> VwOrdOrders { get; set; }

        public virtual DbSet<VwProdProduct> VwProdProducts { get; set; }

        public virtual DbSet<WishWishlist> WishWishlists { get; set; }

        public virtual DbSet<WishWishlistList> WishWishlistLists { get; set; }
        //################################################################
        //  ############################################################
        //################################################################


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //#############################################################
            //  #######################--IDENT--#########################
            //#############################################################
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users", "ident");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_created");
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

                entity.HasIndex(e => e.UserId, "fki_fk__cart_shopcarts__ident_users");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
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

            modelBuilder.Entity<OrdOrder>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("ord_orders_pkey");

                entity.ToTable("ord_orders", "dom");

                entity.HasIndex(e => e.UserId, "fki_fk__ord_orders__users__id");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.UserId).HasColumnName("user_id");
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

            modelBuilder.Entity<ProdProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("prod_products_pkey");

                entity.ToTable("prod_products", "dom");

                entity.Property(e => e.Id)
                    .UseIdentityAlwaysColumn()
                    .HasColumnName("id");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.DateTimeModified)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_modified");
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
            });

            modelBuilder.Entity<VwCartShopcart>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_cart_shopcarts", "dom");

                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.ProductPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("product_price");
                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .HasColumnName("sku");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VwOrdOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_ord_orders", "dom");

                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.OrderPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("order_price");
                entity.Property(e => e.ProductPrice)
                    .HasPrecision(16, 4)
                    .HasColumnName("product_price");
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

                entity.Property(e => e.Curr)
                    .HasMaxLength(3)
                    .HasColumnName("curr");
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateModified).HasColumnName("date_modified");
                entity.Property(e => e.DateTimeCreate).HasColumnName("date_time_create");
                entity.Property(e => e.DateTimeModified).HasColumnName("date_time_modified");
                entity.Property(e => e.Id).HasColumnName("id");
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

            modelBuilder.Entity<WishWishlist>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("wish_wishlists_pkey");

                entity.ToTable("wish_wishlists", "dom");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.DateTimeCreate)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("date_time_create");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<WishWishlistList>(entity =>
            {
                entity.HasKey(e => new { e.WishId, e.ProductId }).HasName("wish_wishlist_lists_pkey");

                entity.ToTable("wish_wishlist_lists", "dom");

                entity.Property(e => e.WishId).HasColumnName("wish_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product).WithMany(p => p.WishWishlistLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__wish_wishlist_lists__prod_products");

                entity.HasOne(d => d.Wish).WithMany(p => p.WishWishlistLists)
                    .HasForeignKey(d => d.WishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk__wish_whishlist_lists__wish_whishlist");
            });
            //################################################################
            //  ############################################################
            //################################################################


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}