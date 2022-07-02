using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AlterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleInfoId",
                table: "menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_UserInfoId",
                table: "role",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_menu_RoleInfoId",
                table: "menu",
                column: "RoleInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_menu_role_RoleInfoId",
                table: "menu",
                column: "RoleInfoId",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_user_UserInfoId",
                table: "role",
                column: "UserInfoId",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menu_role_RoleInfoId",
                table: "menu");

            migrationBuilder.DropForeignKey(
                name: "FK_role_user_UserInfoId",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_role_UserInfoId",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_menu_RoleInfoId",
                table: "menu");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "role");

            migrationBuilder.DropColumn(
                name: "RoleInfoId",
                table: "menu");
        }
    }
}
