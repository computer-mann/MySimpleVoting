using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ExpiredCandidates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Categories",
                nullable: false
                );

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpired",
                table: "Categories",
                nullable: false
                );

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Categories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DateExpired",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Categories");
        }
    }
}
