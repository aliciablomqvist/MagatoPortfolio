// <copyright file="20250411080306_AddMediaUrlsToPageContent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Magato.Api.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AddMediaUrlsToPageContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PageContents",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "ExtraText",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "PageContents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MainText",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrls",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "PageContents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SubText",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraText",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "MainText",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "MediaUrls",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "SubText",
                table: "PageContents");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "PageContents",
                newName: "Value");
        }
    }
}
