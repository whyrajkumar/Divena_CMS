using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Divena_CMS.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Page",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Page",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "Page",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "Page",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "Page",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Page",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Page",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "ValidatedBy",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Order",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldUnicode: false,
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldUnicode: false,
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Media",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Media",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Media",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Media",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alt",
                table: "Media",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Media",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "BlogCategory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "BlogCategory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "BlogCategory",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BlogCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "BlogCategory",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blog",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "Blog",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "Blog",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "Blog",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blog",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Blog",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Page",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Page",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "Page",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "Page",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "Page",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Page",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Page",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "ValidatedBy",
                table: "Order",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Order",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Menu",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Item",
                table: "Menu",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Menu",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Media",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Media",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Media",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Media",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alt",
                table: "Media",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Media",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "BlogCategory",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BlogCategory",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "BlogCategory",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "BlogCategory",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "BlogCategory",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BlogCategory",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "BlogCategory",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Blog",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blog",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MetaTitle",
                table: "Blog",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeyword",
                table: "Blog",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "Blog",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blog",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedOn",
                table: "Blog",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime));
        }
    }
}
