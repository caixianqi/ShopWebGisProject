using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebGisEntityFrameWorkCore.Migrations
{
    public partial class AddassociationUser_Role_Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuInfoRoleInfo",
                columns: table => new
                {
                    MenusId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInfoRoleInfo", x => new { x.MenusId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_MenuInfoRoleInfo_menu_MenusId",
                        column: x => x.MenusId,
                        principalTable: "menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuInfoRoleInfo_role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfoUserInfo",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfoUserInfo", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleInfoUserInfo_role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInfoUserInfo_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuInfoRoleInfo_RolesId",
                table: "MenuInfoRoleInfo",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInfoUserInfo_UsersId",
                table: "RoleInfoUserInfo",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuInfoRoleInfo");

            migrationBuilder.DropTable(
                name: "RoleInfoUserInfo");
        }
    }
}
