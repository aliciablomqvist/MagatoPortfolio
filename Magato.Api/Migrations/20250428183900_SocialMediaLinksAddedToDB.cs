using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magato.Api.Migrations
{
    /// <inheritdoc />
    public partial class SocialMediaLinksAddedToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductInquiries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SocialMediaLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageContentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMediaLink_PageContents_PageContentId",
                        column: x => x.PageContentId,
                        principalTable: "PageContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaLink_PageContentId",
                table: "SocialMediaLink",
                column: "PageContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialMediaLink");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductInquiries");
        }
    }
}
