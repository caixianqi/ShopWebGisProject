using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class UpdateBasicModelDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "user",
                type: "datetime",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "user",
                type: "datetime",
                nullable: true,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "role",
                type: "datetime",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "role",
                type: "datetime",
                nullable: true,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "menu",
                type: "datetime",
                nullable: true,
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "menu",
                type: "datetime",
                nullable: true,
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldComment: "创建时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "user",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "user",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "role",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "role",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "创建时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatetime",
                table: "menu",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "修改时间");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                table: "menu",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldComment: "创建时间");
        }
    }
}
