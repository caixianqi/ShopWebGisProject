using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AddEntityShopClassification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "goodClassification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    parentId = table.Column<int>(type: "int", nullable: false),
                    sort = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_goodClassification", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goodClassification");
        }
    }
}
