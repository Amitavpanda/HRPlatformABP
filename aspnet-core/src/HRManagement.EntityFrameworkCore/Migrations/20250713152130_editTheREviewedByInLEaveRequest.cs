using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagement.Migrations
{
    /// <inheritdoc />
    public partial class editTheREviewedByInLEaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLeaveRequests_AbpUsers_ReviewedBy",
                table: "AppLeaveRequests");

            migrationBuilder.RenameColumn(
                name: "ReviewedBy",
                table: "AppLeaveRequests",
                newName: "HRManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_AppLeaveRequests_ReviewedBy",
                table: "AppLeaveRequests",
                newName: "IX_AppLeaveRequests_HRManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLeaveRequests_AppHRManagers_HRManagerId",
                table: "AppLeaveRequests",
                column: "HRManagerId",
                principalTable: "AppHRManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppLeaveRequests_AppHRManagers_HRManagerId",
                table: "AppLeaveRequests");

            migrationBuilder.RenameColumn(
                name: "HRManagerId",
                table: "AppLeaveRequests",
                newName: "ReviewedBy");

            migrationBuilder.RenameIndex(
                name: "IX_AppLeaveRequests_HRManagerId",
                table: "AppLeaveRequests",
                newName: "IX_AppLeaveRequests_ReviewedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AppLeaveRequests_AbpUsers_ReviewedBy",
                table: "AppLeaveRequests",
                column: "ReviewedBy",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
