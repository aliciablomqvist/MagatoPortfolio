using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magato.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInProductInquieryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHandled",
                table: "ProductInquiries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductInquiries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHandled",
                table: "ProductInquiries");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductInquiries");
        }
    }
}
