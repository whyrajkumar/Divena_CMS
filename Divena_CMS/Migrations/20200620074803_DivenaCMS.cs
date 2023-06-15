using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Divena_CMS.Migrations
{
    public partial class DivenaCMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MetaTitle = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaKeyword = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaDescription = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetalIndex = table.Column<string>(nullable: true),
                    MetalFollow = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    PrimaryImageId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MetaTitle = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaKeyword = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaDescription = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetalIndex = table.Column<string>(nullable: true),
                    MetalFollow = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Url = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Alt = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    Item = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    ValidatedBy = table.Column<string>(maxLength: 10, nullable: true),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MetaTitle = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaKeyword = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetaDescription = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MetalIndex = table.Column<string>(nullable: true),
                    MetalFollow = table.Column<string>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(unicode: false, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Page");
        }
    }
}
