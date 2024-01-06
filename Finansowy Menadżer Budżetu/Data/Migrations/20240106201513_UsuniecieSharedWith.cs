using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finansowy_Menadżer_Budżetu.Data.Migrations
{
    public partial class UsuniecieSharedWith : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedWith",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SharedWith",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
