using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.EzGet.Migrations
{
    public partial class EzGet_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EzGetCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_EzGetCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EzGetNuGetPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Downloads = table.Column<long>(type: "bigint", nullable: false),
                    HasReadme = table.Column<bool>(type: "bit", nullable: false),
                    HasEmbeddedIcon = table.Column<bool>(type: "bit", nullable: false),
                    IsPrerelease = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Listed = table.Column<bool>(type: "bit", nullable: false),
                    MinClientVersion = table.Column<string>(type: "nvarchar(44)", maxLength: 44, nullable: true),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequireLicenseAcceptance = table.Column<bool>(type: "bit", nullable: false),
                    SemVerLevel = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    LicenseUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ProjectUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RepositoryUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RepositoryType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedVersion = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OriginalVersion = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_EzGetNuGetPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EzGetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EzGetCredentialScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GlobPattern = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AllowAction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetCredentialScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EzGetCredentialScopes_EzGetCredentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "EzGetCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EzGetNuGetPackageDependencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DependencyPackageName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    VersionRange = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TargetFramework = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetNuGetPackageDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EzGetNuGetPackageDependencies_EzGetNuGetPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "EzGetNuGetPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EzGetNuGetPackageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetNuGetPackageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EzGetNuGetPackageTypes_EzGetNuGetPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "EzGetNuGetPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EzGetNuGetTargetFrameworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Moniker = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetNuGetTargetFrameworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EzGetNuGetTargetFrameworks_EzGetNuGetPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "EzGetNuGetPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EzGetCredentials_Value",
                table: "EzGetCredentials",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetCredentialScopes_CredentialId",
                table: "EzGetCredentialScopes",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackageDependencies_PackageId",
                table: "EzGetNuGetPackageDependencies",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageName",
                table: "EzGetNuGetPackages",
                column: "PackageName");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion",
                table: "EzGetNuGetPackages",
                columns: new[] { "PackageName", "NormalizedVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackageTypes_PackageId",
                table: "EzGetNuGetPackageTypes",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetTargetFrameworks_PackageId",
                table: "EzGetNuGetTargetFrameworks",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetUsers_TenantId_Email",
                table: "EzGetUsers",
                columns: new[] { "TenantId", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_EzGetUsers_TenantId_UserName",
                table: "EzGetUsers",
                columns: new[] { "TenantId", "UserName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EzGetCredentialScopes");

            migrationBuilder.DropTable(
                name: "EzGetNuGetPackageDependencies");

            migrationBuilder.DropTable(
                name: "EzGetNuGetPackageTypes");

            migrationBuilder.DropTable(
                name: "EzGetNuGetTargetFrameworks");

            migrationBuilder.DropTable(
                name: "EzGetUsers");

            migrationBuilder.DropTable(
                name: "EzGetCredentials");

            migrationBuilder.DropTable(
                name: "EzGetNuGetPackages");
        }
    }
}
