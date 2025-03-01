using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blazorit.Infrastructure.Migrations.DomDb
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cart_shopcart",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_shopcart", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dly_delivery_address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    date_time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dly_delivery_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dly_delivery_method",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    enter_address = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dly_delivery_method", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ord_checkout_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    order_token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    canceled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    payment_amount = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    delivery_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ord_checkout_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pmnt_payment_method",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    method = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_cod = table.Column<bool>(type: "boolean", nullable: false),
                    ordby = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pmnt_payment_method", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prod_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    prefix_sku = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prod_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vw_cart_shopcart",
                columns: table => new
                {
                    cart_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    date_create = table.Column<DateOnly>(type: "date", nullable: true),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    product_id = table.Column<long>(type: "bigint", nullable: true),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    curr = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    product_price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    product_link_part = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    date_time_item_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_dly_delivery",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: true),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    method_id = table.Column<long>(type: "bigint", nullable: true),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    delivery_cost = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    delivery_date = table.Column<DateOnly>(type: "date", nullable: true),
                    delivery_time_start = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true),
                    delivery_time_end = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_dly_methods_address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: true),
                    method_id = table.Column<long>(type: "bigint", nullable: true),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_dly_user_delivery",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    date_time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_ord_order",
                columns: table => new
                {
                    order_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    delivery_id = table.Column<long>(type: "bigint", nullable: true),
                    date_create = table.Column<DateOnly>(type: "date", nullable: true),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    product_id = table.Column<long>(type: "bigint", nullable: true),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    curr = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    product_price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    product_link_part = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    order_price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "vw_prod_product",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: true),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    curr = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    date_create = table.Column<DateOnly>(type: "date", nullable: true),
                    date_modified = table.Column<DateOnly>(type: "date", nullable: true),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    date_time_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    link_part = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_on_site = table.Column<bool>(type: "boolean", nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    category_full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "wish_wish",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wish_wish", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dly_methods_address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    method_id = table.Column<long>(type: "bigint", nullable: false),
                    address_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dly_methods_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_dly_methods_address_dly_delivery_address_address_id",
                        column: x => x.address_id,
                        principalTable: "dly_delivery_address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dly_methods_address_dly_delivery_method_method_id",
                        column: x => x.method_id,
                        principalTable: "dly_delivery_method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dly_user_delivery",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    method_id = table.Column<long>(type: "bigint", nullable: false),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dly_user_delivery", x => x.id);
                    table.ForeignKey(
                        name: "fk_dly_user_delivery_dly_delivery_address_address_id",
                        column: x => x.address_id,
                        principalTable: "dly_delivery_address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dly_user_delivery_dly_delivery_method_method_id",
                        column: x => x.method_id,
                        principalTable: "dly_delivery_method",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pmnt_payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_amount = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    checkout_order_id = table.Column<long>(type: "bigint", nullable: false),
                    order_token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    payment_info = table.Column<string>(type: "text", nullable: true),
                    is_paid = table.Column<bool>(type: "boolean", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pmnt_payment", x => x.id);
                    table.ForeignKey(
                        name: "fk_pmnt_payment_pmnt_payment_method_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "pmnt_payment_method",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prod_product",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    curr = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    date_time_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    description = table.Column<string>(type: "text", nullable: true),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    link_part = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValueSql: "'empty'::character varying"),
                    is_on_site = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prod_product", x => x.id);
                    table.ForeignKey(
                        name: "fk_prod_product_prod_category_category_id",
                        column: x => x.category_id,
                        principalTable: "prod_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dly_delivery",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    user_delivery_id = table.Column<long>(type: "bigint", nullable: false),
                    delivery_cost = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    delivery_date = table.Column<DateOnly>(type: "date", nullable: true),
                    delivery_time_start = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true),
                    delivery_time_end = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dly_delivery", x => x.id);
                    table.ForeignKey(
                        name: "fk_dly_delivery_dly_user_delivery_user_delivery_id",
                        column: x => x.user_delivery_id,
                        principalTable: "dly_user_delivery",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart_shopcart_list",
                columns: table => new
                {
                    cart_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    date_time_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_shopcart_list", x => new { x.cart_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_cart_shopcart_list_cart_shopcart_cart_id",
                        column: x => x.cart_id,
                        principalTable: "cart_shopcart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cart_shopcart_list_prod_product_product_id",
                        column: x => x.product_id,
                        principalTable: "prod_product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prod_picture",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    link_part = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    pic_size = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValueSql: "'medium'::character varying"),
                    order_num = table.Column<short>(type: "smallint", nullable: false),
                    site_location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'site'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prod_picture", x => x.id);
                    table.ForeignKey(
                        name: "fk_prod_picture_prod_product_product_id",
                        column: x => x.product_id,
                        principalTable: "prod_product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "wish_wish_list",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wish_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_wish_wish_list", x => x.id);
                    table.ForeignKey(
                        name: "fk_wish_wish_list_prod_product_product_id",
                        column: x => x.product_id,
                        principalTable: "prod_product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_wish_wish_list_wish_wish_wish_id",
                        column: x => x.wish_id,
                        principalTable: "wish_wish",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ord_order",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    date_time_create = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    delivery_id = table.Column<long>(type: "bigint", nullable: false),
                    payment_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ord_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_ord_order_dly_delivery_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "dly_delivery",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ord_order_pmnt_payment_payment_id",
                        column: x => x.payment_id,
                        principalTable: "pmnt_payment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ord_order_list",
                columns: table => new
                {
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: true),
                    id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ord_order_list", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_ord_order_list_ord_order_order_id",
                        column: x => x.order_id,
                        principalTable: "ord_order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ord_order_list_prod_product_product_id",
                        column: x => x.product_id,
                        principalTable: "prod_product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_cart_shopcart_user_id",
                table: "cart_shopcart",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cart_shopcart_list_product_id",
                table: "cart_shopcart_list",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_delivery_user_delivery_id",
                table: "dly_delivery",
                column: "user_delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_methods_address_address_id",
                table: "dly_methods_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_methods_address_method_id_address_id",
                table: "dly_methods_address",
                columns: new[] { "method_id", "address_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_dly_user_delivery_address_id",
                table: "dly_user_delivery",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_user_delivery_method_id",
                table: "dly_user_delivery",
                column: "method_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_user_delivery_user_id",
                table: "dly_user_delivery",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_dly_user_delivery_user_id_method_id_address_id",
                table: "dly_user_delivery",
                columns: new[] { "user_id", "method_id", "address_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ord_checkout_order_order_token",
                table: "ord_checkout_order",
                column: "order_token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ord_checkout_order_order_token_canceled",
                table: "ord_checkout_order",
                columns: new[] { "order_token", "canceled" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ord_order_delivery_id",
                table: "ord_order",
                column: "delivery_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ord_order_payment_id",
                table: "ord_order",
                column: "payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ord_order_user_id",
                table: "ord_order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ord_order_list_order_id",
                table: "ord_order_list",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_ord_order_list_product_id",
                table: "ord_order_list",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_pmnt_payment_checkout_order_id",
                table: "pmnt_payment",
                column: "checkout_order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_pmnt_payment_payment_method_id",
                table: "pmnt_payment",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_prod_category_name",
                table: "prod_category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prod_picture_product_id_pic_size_site_location",
                table: "prod_picture",
                columns: new[] { "product_id", "pic_size", "site_location" });

            migrationBuilder.CreateIndex(
                name: "ix_prod_picture_product_id_pic_size_site_location_order_num",
                table: "prod_picture",
                columns: new[] { "product_id", "pic_size", "site_location", "order_num" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prod_product_category_id",
                table: "prod_product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_prod_product_link_part_category_id",
                table: "prod_product",
                columns: new[] { "link_part", "category_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_prod_product_sku",
                table: "prod_product",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wish_wish_user_id",
                table: "wish_wish",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_wish_wish_list_product_id",
                table: "wish_wish_list",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_wish_wish_list_wish_id",
                table: "wish_wish_list",
                column: "wish_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_shopcart_list");

            migrationBuilder.DropTable(
                name: "dly_methods_address");

            migrationBuilder.DropTable(
                name: "ord_checkout_order");

            migrationBuilder.DropTable(
                name: "ord_order_list");

            migrationBuilder.DropTable(
                name: "prod_picture");

            migrationBuilder.DropTable(
                name: "vw_cart_shopcart");

            migrationBuilder.DropTable(
                name: "vw_dly_delivery");

            migrationBuilder.DropTable(
                name: "vw_dly_methods_address");

            migrationBuilder.DropTable(
                name: "vw_dly_user_delivery");

            migrationBuilder.DropTable(
                name: "vw_ord_order");

            migrationBuilder.DropTable(
                name: "vw_prod_product");

            migrationBuilder.DropTable(
                name: "wish_wish_list");

            migrationBuilder.DropTable(
                name: "cart_shopcart");

            migrationBuilder.DropTable(
                name: "ord_order");

            migrationBuilder.DropTable(
                name: "prod_product");

            migrationBuilder.DropTable(
                name: "wish_wish");

            migrationBuilder.DropTable(
                name: "dly_delivery");

            migrationBuilder.DropTable(
                name: "pmnt_payment");

            migrationBuilder.DropTable(
                name: "prod_category");

            migrationBuilder.DropTable(
                name: "dly_user_delivery");

            migrationBuilder.DropTable(
                name: "pmnt_payment_method");

            migrationBuilder.DropTable(
                name: "dly_delivery_address");

            migrationBuilder.DropTable(
                name: "dly_delivery_method");
        }
    }
}
