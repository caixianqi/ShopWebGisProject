using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class initdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    rolename = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    RoleDescription = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    RoleShot = table.Column<int>(type: "int", nullable: false),
                    issoftdelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    createuserid = table.Column<string>(type: "text", nullable: false, comment: "创建用户Id"),
                    createusername = table.Column<string>(type: "text", nullable: false, comment: "创建用户名称"),
                    updateusername = table.Column<string>(type: "text", nullable: false, comment: "更新操作用户名称"),
                    updateuserid = table.Column<string>(type: "text", nullable: false, comment: "更新操作用户Id"),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    updatetime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "用户名"),
                    userpassword = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "用户密码"),
                    userphone = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, comment: "用户号码"),
                    province = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "省份"),
                    city = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "城市"),
                    county = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, comment: "区县镇"),
                    addressdetail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "详细地址"),
                    issoftdelete = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否删除"),
                    createuserid = table.Column<string>(type: "text", nullable: false, comment: "创建用户Id"),
                    createusername = table.Column<string>(type: "text", nullable: false, comment: "创建用户名称"),
                    updateusername = table.Column<string>(type: "text", nullable: false, comment: "更新操作用户名称"),
                    updateuserid = table.Column<string>(type: "text", nullable: false, comment: "更新操作用户Id"),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "创建时间"),
                    updatetime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                },
                comment: "用户");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
