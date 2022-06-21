using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class alterUserPasswordLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userpassword",
                table: "user",
                type: "text",
                nullable: false,
                comment: "用户密码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldComment: "用户密码");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userpassword",
                table: "user",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                comment: "用户密码",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "用户密码");
        }
    }
}
