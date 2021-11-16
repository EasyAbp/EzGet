using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.EzGet.Migrations
{
    public partial class Modify_Feed_Add_UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "EzGetFeeds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EzGetFeeds");
        }
    }
}
