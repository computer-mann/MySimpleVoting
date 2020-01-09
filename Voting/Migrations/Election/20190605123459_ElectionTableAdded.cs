using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ElectionTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PollEndDate",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Election",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    DateClosed = table.Column<DateTime>(nullable: false),
                    Ongoing = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Election", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ElectionId",
                table: "Categories",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Election_ElectionId",
                table: "Categories",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Election_ElectionId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Election");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ElectionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Categories");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PollEndDate",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
