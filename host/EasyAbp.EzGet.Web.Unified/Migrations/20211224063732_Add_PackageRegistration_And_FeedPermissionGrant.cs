using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.EzGet.Migrations
{
    public partial class Add_PackageRegistration_And_FeedPermissionGrant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion_FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.AddColumn<Guid>(
                name: "PackageRegistrationId",
                table: "EzGetNuGetPackages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EzGetFeedPermissionGrants",
                columns: table => new
                {
                    FeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetFeedPermissionGrants", x => new { x.FeedId, x.ProviderName, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "EzGetPackageRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PackageName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DownloadCount = table.Column<long>(type: "bigint", nullable: false),
                    PackageType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetPackageRegistrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion_PackageRegistrationId",
                table: "EzGetNuGetPackages",
                columns: new[] { "PackageName", "NormalizedVersion", "PackageRegistrationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageRegistrationId",
                table: "EzGetNuGetPackages",
                column: "PackageRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetPackageRegistrations_FeedId",
                table: "EzGetPackageRegistrations",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetPackageRegistrations_PackageName_PackageType_FeedId",
                table: "EzGetPackageRegistrations",
                columns: new[] { "PackageName", "PackageType", "FeedId" },
                unique: true,
                filter: "[PackageName] IS NOT NULL AND [PackageType] IS NOT NULL AND [FeedId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EzGetFeedPermissionGrants");

            migrationBuilder.DropTable(
                name: "EzGetPackageRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion_PackageRegistrationId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_PackageRegistrationId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropColumn(
                name: "PackageRegistrationId",
                table: "EzGetNuGetPackages");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedId",
                table: "EzGetNuGetPackages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_FeedId",
                table: "EzGetNuGetPackages",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion_FeedId",
                table: "EzGetNuGetPackages",
                columns: new[] { "PackageName", "NormalizedVersion", "FeedId" },
                unique: true,
                filter: "[PackageName] IS NOT NULL AND [NormalizedVersion] IS NOT NULL AND [FeedId] IS NOT NULL");
        }
    }
}
