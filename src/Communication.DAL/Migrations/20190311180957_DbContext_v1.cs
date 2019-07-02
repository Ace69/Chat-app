using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ObjectDesign.Migrations
{
    public partial class DbContext_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Reactions_ReactionId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ReactionId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReactionId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReactionId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReactionId",
                table: "Comments",
                column: "ReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Reactions_ReactionId",
                table: "Comments",
                column: "ReactionId",
                principalTable: "Reactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
