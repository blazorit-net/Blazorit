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

        public virtual DbSet<ProdCategory> ProdCategories { get; set; }

        public virtual DbSet<ProdPicture> ProdPictures { get; set; }

        public virtual DbSet<ProdProduct> ProdProducts { get; set; }

        public virtual DbSet<VwCartShopcart> VwCartShopcarts { get; set; }

        public virtual DbSet<VwOrdOrder> VwOrdOrders { get; set; }

        public virtual DbSet<VwProdProduct> VwProdProducts { get; set; }

        public virtual DbSet<WishWish> WishWishes { get; set; }

        public virtual DbSet<WishWishList> WishWishLists { get; set; }
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
                entity.Property(e => e.DateTimeCreated).HasColumnName("date_time_created");
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