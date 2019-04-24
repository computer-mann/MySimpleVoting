using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class AddedMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Candidates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);
        }
    }
}
