using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class alterUserColumnCancelRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userpassword",
                table: "user",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                comment: "用户密码",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldComment: "用户密码");

            migrationBuilder.AlterColumn<string>(
                name: "updateusername",
                table: "user",
                type: "text",
                nullable: true,
                comment: "更新操作用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "更新操作用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "updateuserid",
                table: "user",
                type: "text",
                nullable: true,
                comment: "更新操作用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "更新操作用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "createusername",
                table: "user",
                type: "text",
                nullable: true,
                comment: "创建用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "创建用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "createuserid",
                table: "user",
                type: "text",
                nullable: true,
                comment: "创建用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "创建用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "updateusername",
                table: "role",
                type: "text",
                nullable: true,
                comment: "更新操作用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "更新操作用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "updateuserid",
                table: "role",
                type: "text",
                nullable: true,
                comment: "更新操作用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "更新操作用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "createusername",
                table: "role",
                type: "text",
                nullable: true,
                comment: "创建用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "创建用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "createuserid",
                table: "role",
                type: "text",
                nullable: true,
                comment: "创建用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "创建用户Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userpassword",
                table: "user",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "用户密码",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldComment: "用户密码");

            migrationBuilder.AlterColumn<string>(
                name: "updateusername",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "更新操作用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "更新操作用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "updateuserid",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "更新操作用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "更新操作用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "createusername",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "创建用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "创建用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "createuserid",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "创建用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "创建用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "updateusername",
                table: "role",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "更新操作用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "更新操作用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "updateuserid",
                table: "role",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "更新操作用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "更新操作用户Id");

            migrationBuilder.AlterColumn<string>(
                name: "createusername",
                table: "role",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "创建用户名称",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "创建用户名称");

            migrationBuilder.AlterColumn<string>(
                name: "createuserid",
                table: "role",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "创建用户Id",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "创建用户Id");
        }
    }
}
