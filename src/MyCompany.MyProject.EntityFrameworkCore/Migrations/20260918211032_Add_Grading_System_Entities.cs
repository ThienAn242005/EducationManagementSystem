using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCompany.MyProject.Migrations
{
    /// <inheritdoc />
    public partial class Add_Grading_System_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QlsGradeLockStatuses_Teachers_LockedByTeacherId",
                table: "QlsGradeLockStatuses");

            migrationBuilder.RenameColumn(
                name: "LockedByTeacherId",
                table: "QlsGradeLockStatuses",
                newName: "LockedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_QlsGradeLockStatuses_LockedByTeacherId",
                table: "QlsGradeLockStatuses",
                newName: "IX_QlsGradeLockStatuses_LockedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "Performance",
                table: "GradeRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_QlsGradeLockStatuses_Teachers_LockedByUserId",
                table: "QlsGradeLockStatuses",
                column: "LockedByUserId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QlsGradeLockStatuses_Teachers_LockedByUserId",
                table: "QlsGradeLockStatuses");

            migrationBuilder.DropColumn(
                name: "Performance",
                table: "GradeRecords");

            migrationBuilder.RenameColumn(
                name: "LockedByUserId",
                table: "QlsGradeLockStatuses",
                newName: "LockedByTeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_QlsGradeLockStatuses_LockedByUserId",
                table: "QlsGradeLockStatuses",
                newName: "IX_QlsGradeLockStatuses_LockedByTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_QlsGradeLockStatuses_Teachers_LockedByTeacherId",
                table: "QlsGradeLockStatuses",
                column: "LockedByTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
