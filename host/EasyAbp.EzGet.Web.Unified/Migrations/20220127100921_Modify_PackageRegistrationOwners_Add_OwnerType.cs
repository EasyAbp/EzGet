using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.EzGet.Migrations
{
    public partial class Modify_PackageRegistrationOwners_Add_OwnerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerType",
                table: "EzGetPackageRegistrationOwners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerType",
                table: "EzGetPackageRegistrationOwners");
        }
    }
}
