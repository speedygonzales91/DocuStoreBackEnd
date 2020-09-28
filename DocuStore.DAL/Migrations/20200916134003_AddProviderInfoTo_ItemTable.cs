using Microsoft.EntityFrameworkCore.Migrations;

namespace DocuStore.DAL.Migrations
{
    public partial class AddProviderInfoTo_ItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "Items",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Items");
        }
    }
}
