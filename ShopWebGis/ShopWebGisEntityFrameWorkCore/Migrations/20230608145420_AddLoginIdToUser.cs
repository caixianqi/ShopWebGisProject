using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AddLoginIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "openid",
                table: "user",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "公开Id");

            migrationBuilder.AddColumn<string>(
                name: "registrationtype",
                table: "user",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "注册方式");

            migrationBuilder.AddColumn<string>(
                name: "userheadportrait",
                table: "user",
                type: "text",
                nullable: true,
                comment: "用户头像");

            migrationBuilder.AddColumn<string>(
                name: "userloginid",
                table: "user",
                type: "varchar(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "",
                comment: "用户登录名");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "openid",
                table: "user");

            migrationBuilder.DropColumn(
                name: "registrationtype",
                table: "user");

            migrationBuilder.DropColumn(
                name: "userheadportrait",
                table: "user");

            migrationBuilder.DropColumn(
                name: "userloginid",
                table: "user");
        }
    }
}
