using Microsoft.EntityFrameworkCore.Migrations;

namespace KooBooKMVC.Migrations
{
    public partial class AddedshortDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Recipes",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Recipes");
        }
    }
}
