using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Employees_EmployeId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "EmployeId",
                table: "Addresses",
                newName: "employeId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_EmployeId",
                table: "Addresses",
                newName: "IX_Addresses_employeId");

            migrationBuilder.AlterColumn<int>(
                name: "employeId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Employees_employeId",
                table: "Addresses",
                column: "employeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Employees_employeId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "employeId",
                table: "Addresses",
                newName: "EmployeId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_employeId",
                table: "Addresses",
                newName: "IX_Addresses_EmployeId");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeId",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Employees_EmployeId",
                table: "Addresses",
                column: "EmployeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
