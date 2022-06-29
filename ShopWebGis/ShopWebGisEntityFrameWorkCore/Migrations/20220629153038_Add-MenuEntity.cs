using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AddMenuEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "菜单名"),
                    parentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "父菜单Id"),
                    sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    url = table.Column<string>(type: "text", nullable: false, comment: "Url 对应前端路由"),
                    issoftdelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    createuserid = table.Column<string>(type: "text", nullable: true, comment: "创建用户Id"),
                    createusername = table.Column<string>(type: "text", nullable: true, comment: "创建用户名称"),
                    updateusername = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户名称"),
                    updateuserid = table.Column<string>(type: "text", nullable: true, comment: "更新操作用户Id"),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    updatetime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                },
                comment: "菜单");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu");
        }
    }
}
