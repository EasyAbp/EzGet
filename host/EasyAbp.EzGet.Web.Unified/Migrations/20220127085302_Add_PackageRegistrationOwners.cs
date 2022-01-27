using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.EzGet.Migrations
{
    public partial class Add_PackageRegistrationOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EzGetPackageRegistrationOwners",
                columns: table => new
                {
                    PackageRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetPackageRegistrationOwners", x => new { x.PackageRegistrationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EzGetPackageRegistrationOwners_EzGetPackageRegistrations_PackageRegistrationId",
                        column: x => x.PackageRegistrationId,
                        principalTable: "EzGetPackageRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EzGetPackageRegistrationOwners");
        }
    }
}
