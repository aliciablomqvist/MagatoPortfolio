// <copyright file="20250415091906_ProductInqueryToDb.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Magato.Api.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class ProductInqueryToDb : Migration
{
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
            migrationBuilder.CreateTable(
                name: "ProductInquiries",
                columns: table => new
{
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
{
                    table.PrimaryKey("PK_ProductInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInquiries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInquiries_ProductId",
                table: "ProductInquiries",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
            migrationBuilder.DropTable(
                name: "ProductInquiries");
        }
    }
}
