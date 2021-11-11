using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.EzGet.Migrations
{
    public partial class Add_Credential_Feed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EzGetCredentialScopes",
                table: "EzGetCredentialScopes");

            migrationBuilder.DropIndex(
                name: "IX_EzGetCredentialScopes_CredentialId",
                table: "EzGetCredentialScopes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EzGetCredentialScopes");

            migrationBuilder.DropColumn(
                name: "GlobPattern",
                table: "EzGetCredentialScopes");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedId",
                table: "EzGetNuGetPackages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobPattern",
                table: "EzGetCredentials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EzGetCredentialScopes",
                table: "EzGetCredentialScopes",
                columns: new[] { "CredentialId", "AllowAction" });

            migrationBuilder.CreateTable(
                name: "EzGetFeeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FeedType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_EzGetFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EzGetFeedCredentials",
                columns: table => new
                {
                    FeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EzGetFeedCredentials", x => new { x.FeedId, x.CredentialId });
                    table.ForeignKey(
                        name: "FK_EzGetFeedCredentials_EzGetFeeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "EzGetFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EzGetFeeds_FeedName",
                table: "EzGetFeeds",
                column: "FeedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EzGetFeedCredentials");

            migrationBuilder.DropTable(
                name: "EzGetFeeds");

            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion_FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EzGetCredentialScopes",
                table: "EzGetCredentialScopes");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "EzGetNuGetPackages");

            migrationBuilder.DropColumn(
                name: "GlobPattern",
                table: "EzGetCredentials");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EzGetCredentialScopes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "GlobPattern",
                table: "EzGetCredentialScopes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EzGetCredentialScopes",
                table: "EzGetCredentialScopes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EzGetNuGetPackages_PackageName_NormalizedVersion",
                table: "EzGetNuGetPackages",
                columns: new[] { "PackageName", "NormalizedVersion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EzGetCredentialScopes_CredentialId",
                table: "EzGetCredentialScopes",
                column: "CredentialId");
        }
    }
}
