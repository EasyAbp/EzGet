using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.EzGet.Migrations
{
    public partial class PackageRegistration_Add_LastVersion_Size_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EzGetPackageRegistrations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastVersion",
                table: "EzGetPackageRegistrations",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "EzGetPackageRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EzGetPackageRegistrations",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "EzGetPackageRegistrations");

            migrationBuilder.DropColumn(
                name: "LastVersion",
                table: "EzGetPackageRegistrations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "EzGetPackageRegistrations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EzGetPackageRegistrations");
        }
    }
}
