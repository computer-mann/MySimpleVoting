using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ChangedAreadyVotedKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AlreadyVoted");

            migrationBuilder.AlterColumn<string>(
                name: "Student",
                table: "AlreadyVoted",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted",
                column: "Student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted");

            migrationBuilder.AlterColumn<string>(
                name: "Student",
                table: "AlreadyVoted",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AlreadyVoted",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlreadyVoted",
                table: "AlreadyVoted",
                column: "Id");
        }
    }
}
