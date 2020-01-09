using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class AlreadyVotedKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted");

            migrationBuilder.AddColumn<Guid>(
                name: "ElectionId",
                table: "AlreadyVoted",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted",
                columns: new[] { "Student", "ElectionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "AlreadyVoted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted",
                column: "Student");
        }
    }
}
