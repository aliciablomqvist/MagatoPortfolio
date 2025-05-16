// <copyright file="20250401151852_AddDescriptionToLookbookImage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Magato.Api.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AddDescriptionToLookbookImage : Migration
{
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
            migrationBuilder.CreateTable(
                name: "LookbookImages",
                columns: table => new
{
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
{
                    table.PrimaryKey("PK_LookbookImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookbookImages_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LookbookImages_CollectionId",
                table: "LookbookImages",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
            migrationBuilder.DropTable(
                name: "LookbookImages");
        }
    }
}
