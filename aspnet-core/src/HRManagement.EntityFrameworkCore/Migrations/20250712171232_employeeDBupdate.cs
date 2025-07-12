using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagement.Migrations
{
    /// <inheritdoc />
    public partial class employeeDBupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeductionPerDay",
                table: "AppEmployees",
                type: "decimal(18,2)",
                maxLength: 1000,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SickLeaveBalance",
                table: "AppEmployees",
                type: "decimal(18,2)",
                maxLength: 1000,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnpaidLeaveBalance",
                table: "AppEmployees",
                type: "decimal(18,2)",
                maxLength: 1000,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeductionPerDay",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "SickLeaveBalance",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "UnpaidLeaveBalance",
                table: "AppEmployees");
        }
    }
}
