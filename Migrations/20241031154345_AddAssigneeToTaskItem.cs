using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddAssigneeToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "TaskItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_AssigneeId",
                table: "TaskItems",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_AssigneeId",
                table: "TaskItems",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_AssigneeId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_AssigneeId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "TaskItems");
        }
    }
}
