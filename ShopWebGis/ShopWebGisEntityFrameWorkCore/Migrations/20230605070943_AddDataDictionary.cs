using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AddDataDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "menu",
                type: "varchar(767)",
                nullable: false,
                comment: "Url 对应前端路由",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Url 对应前端路由");

            migrationBuilder.CreateTable(
                name: "datadictionary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "varchar(767)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    issoftdelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    createuserid = table.Column<string>(type: "text", nullable: true, comment: "创建用户Id"),
                    createusername = table.Column<string>(type: "text", nullable: true, comment: "创建用户名称"),
                    updateusername = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户名称"),
                    updateuserid = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户Id"),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "创建时间"),
                    updatetime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datadictionary", x => x.id);
                },
                comment: "数据字典");

            migrationBuilder.CreateTable(
                name: "datadictionaryitem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "varchar(767)", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    isenabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    rank = table.Column<int>(type: "int", nullable: false),
                    datadictionaryid = table.Column<int>(type: "int", nullable: false),
                    issoftdelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    createuserid = table.Column<string>(type: "text", nullable: true, comment: "创建用户Id"),
                    createusername = table.Column<string>(type: "text", nullable: true, comment: "创建用户名称"),
                    updateusername = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户名称"),
                    updateuserid = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户Id"),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "创建时间"),
                    updatetime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datadictionaryitem", x => x.id);
                },
                comment: "数据字典项");

            migrationBuilder.CreateIndex(
                name: "Index_Url",
                table: "menu",
                column: "url");

            migrationBuilder.CreateIndex(
                name: "Code_Unique_Index",
                table: "datadictionary",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Code_DataDictionaryId_Unique_Index",
                table: "datadictionaryitem",
                columns: new[] { "code", "datadictionaryid" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datadictionary");

            migrationBuilder.DropTable(
                name: "datadictionaryitem");

            migrationBuilder.DropIndex(
                name: "Index_Url",
                table: "menu");

            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "menu",
                type: "text",
                nullable: false,
                comment: "Url 对应前端路由",
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldComment: "Url 对应前端路由");
        }
    }
}
