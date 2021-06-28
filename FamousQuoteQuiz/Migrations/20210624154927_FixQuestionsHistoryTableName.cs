using Microsoft.EntityFrameworkCore.Migrations;

namespace FamousQuoteQuiz.Migrations
{
    public partial class FixQuestionsHistoryTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestonsHistory_AspNetUsers_UserId",
                table: "UserQuestonsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestonsHistory_Questions_QuestionId",
                table: "UserQuestonsHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuestonsHistory",
                table: "UserQuestonsHistory");

            migrationBuilder.RenameTable(
                name: "UserQuestonsHistory",
                newName: "UserQuestionsHistory");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuestonsHistory_UserId",
                table: "UserQuestionsHistory",
                newName: "IX_UserQuestionsHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuestonsHistory_QuestionId",
                table: "UserQuestionsHistory",
                newName: "IX_UserQuestionsHistory_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuestionsHistory",
                table: "UserQuestionsHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestionsHistory_AspNetUsers_UserId",
                table: "UserQuestionsHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestionsHistory_Questions_QuestionId",
                table: "UserQuestionsHistory",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestionsHistory_AspNetUsers_UserId",
                table: "UserQuestionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestionsHistory_Questions_QuestionId",
                table: "UserQuestionsHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuestionsHistory",
                table: "UserQuestionsHistory");

            migrationBuilder.RenameTable(
                name: "UserQuestionsHistory",
                newName: "UserQuestonsHistory");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuestionsHistory_UserId",
                table: "UserQuestonsHistory",
                newName: "IX_UserQuestonsHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuestionsHistory_QuestionId",
                table: "UserQuestonsHistory",
                newName: "IX_UserQuestonsHistory_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuestonsHistory",
                table: "UserQuestonsHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestonsHistory_AspNetUsers_UserId",
                table: "UserQuestonsHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestonsHistory_Questions_QuestionId",
                table: "UserQuestonsHistory",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
