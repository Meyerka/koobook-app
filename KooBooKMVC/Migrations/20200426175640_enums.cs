using Microsoft.EntityFrameworkCore.Migrations;

namespace KooBooKMVC.Migrations
{
    public partial class enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealType",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Measurement",
                table: "RecipeComponents");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "RecipeComponents",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "RecipeComponents");

            migrationBuilder.AddColumn<string>(
                name: "MealType",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Measurement",
                table: "RecipeComponents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
