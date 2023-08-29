using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class lang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "RoleNameEng");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "ProductNameEng");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "ProductNameBraz");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "CategoryNameEng");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brands",
                newName: "BrandNameEng");

            migrationBuilder.AddColumn<string>(
                name: "RoleNameBraz",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionBraz",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEng",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoryNameBraz",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrandNameBraz",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleNameBraz",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DescriptionBraz",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionEng",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryNameBraz",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "BrandNameBraz",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "RoleNameEng",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "ProductNameEng",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "ProductNameBraz",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CategoryNameEng",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "BrandNameEng",
                table: "Brands",
                newName: "BrandName");
        }
    }
}
