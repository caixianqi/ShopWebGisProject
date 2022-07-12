using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class MenuEntityAddColumnIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "menu",
                type: "text",
                nullable: true,
                comment: "菜单图标");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "menu");
        }
    }
}
