﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Voting.Migrations.Election
{
    public partial class ExpiredCandidatesAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Categories",
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Local)
                );

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PollEndDate",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
