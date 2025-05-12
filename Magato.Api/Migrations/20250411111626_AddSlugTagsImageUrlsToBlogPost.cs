// <copyright file="20250411111626_AddSlugTagsImageUrlsToBlogPost.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magato.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugTagsImageUrlsToBlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrls",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BlogPosts");
        }
    }
}
