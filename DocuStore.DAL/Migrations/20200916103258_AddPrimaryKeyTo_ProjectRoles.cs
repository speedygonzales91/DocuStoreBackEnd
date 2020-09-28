using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DocuStore.DAL.Migrations
{
    public partial class AddPrimaryKeyTo_ProjectRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectRoles",
                table: "ProjectRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProjectRoles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectRoles",
                table: "ProjectRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_ProjectId",
                table: "ProjectRoles",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectRoles",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_ProjectId",
                table: "ProjectRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProjectRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectRoles",
                table: "ProjectRoles",
                columns: new[] { "ProjectId", "RoleId" });
        }
    }
}
