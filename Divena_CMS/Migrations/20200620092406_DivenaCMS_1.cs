using Microsoft.EntityFrameworkCore.Migrations;

namespace Divena_CMS.Migrations
{
    public partial class DivenaCMS_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetalFollow",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "MetalIndex",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "MetalFollow",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "MetalIndex",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "MetalFollow",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetalIndex",
                table: "Blog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetalFollow",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalIndex",
                table: "Page",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalFollow",
                table: "BlogCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalIndex",
                table: "BlogCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalFollow",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetalIndex",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
