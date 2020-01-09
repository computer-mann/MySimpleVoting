using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Identity
{
    public partial class AddVoterThings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UsersIdentity",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UsersIdentity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UsersIdentity",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherNames",
                table: "UsersIdentity",
                maxLength: 35,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UsersIdentity");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "UsersIdentity");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UsersIdentity");

            migrationBuilder.DropColumn(
                name: "OtherNames",
                table: "UsersIdentity");
        }
    }
}
