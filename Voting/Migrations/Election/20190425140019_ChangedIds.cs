using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ChangedIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Categories_CategoryId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Votes",
                newName: "CategoryCatId");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Votes",
                newName: "CandidateCanId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_CategoryId",
                table: "Votes",
                newName: "IX_Votes_CategoryCatId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                newName: "IX_Votes_CandidateCanId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CatId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Candidates",
                newName: "CategoryCatId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Candidates",
                newName: "CanId");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_CategoryId",
                table: "Candidates",
                newName: "IX_Candidates_CategoryCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Categories_CategoryCatId",
                table: "Candidates",
                column: "CategoryCatId",
                principalTable: "Categories",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Categories_CategoryCatId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidateCanId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Categories_CategoryCatId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "CategoryCatId",
                table: "Votes",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CandidateCanId",
                table: "Votes",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_CategoryCatId",
                table: "Votes",
                newName: "IX_Votes_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_CandidateCanId",
                table: "Votes",
                newName: "IX_Votes_CandidateId");

            migrationBuilder.RenameColumn(
                name: "CatId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryCatId",
                table: "Candidates",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CanId",
                table: "Candidates",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Candidates_CategoryCatId",
                table: "Candidates",
                newName: "IX_Candidates_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Categories_CategoryId",
                table: "Candidates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Categories_CategoryId",
                table: "Votes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
