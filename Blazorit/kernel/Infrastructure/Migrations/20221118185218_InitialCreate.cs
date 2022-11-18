using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blazorit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ident");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "ident",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(name: "user_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    passwordhash = table.Column<byte[]>(name: "password_hash", type: "bytea", nullable: false),
                    passwordsalt = table.Column<byte[]>(name: "password_salt", type: "bytea", nullable: false),
                    datecreated = table.Column<DateTime>(name: "date_created", type: "timestamp without time zone", nullable: false),
                    userrole = table.Column<string>(name: "user_role", type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users",
                schema: "ident");
        }
    }
}
