using Microsoft.EntityFrameworkCore.Migrations;

namespace PW_Proj.Migrations
{
    public partial class InitaialPwDataBaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    CofirmPassword = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CofirmPassword", "Email", "Gender", "Password", "UserName" },
                values: new object[] { 1, "simospassword", "SimoCaldarasul@gmail.com", 0, "simospassword", "SimoSim" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CofirmPassword", "Email", "Gender", "Password", "UserName" },
                values: new object[] { 2, "tabilove", "TabitaBadGirl69@gmail.com", 1, "tabilove", "TabitaLove" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
