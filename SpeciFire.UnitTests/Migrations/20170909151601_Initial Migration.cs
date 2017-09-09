using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeciFire.UnitTests.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address_City = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Name_FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    Name_LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
