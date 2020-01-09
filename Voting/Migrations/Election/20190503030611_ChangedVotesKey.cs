using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ChangedVotesKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidateCanId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Categories_CategoryCatId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_CandidateCanId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_CategoryCatId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CandidateCanId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CategoryCatId",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Votes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "Votes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                columns: new[] { "CategoryId", "CandidateId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Categories_CategoryId",
                table: "Votes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Categories_CategoryId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Votes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Votes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "CandidateCanId",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryCatId",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateCanId",
                table: "Votes",
                column: "CandidateCanId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CategoryCatId",
                table: "Votes",
                column: "CategoryCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Candidates_CandidateCanId",
                table: "Votes",
                column: "CandidateCanId",
                principalTable: "Candidates",
                principalColumn: "CanId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Categories_CategoryCatId",
                table: "Votes",
                column: "CategoryCatId",
                principalTable: "Categories",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
