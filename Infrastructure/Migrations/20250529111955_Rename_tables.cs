using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rename_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduction_Programs_ProductionsId",
                table: "GenreProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Programs_ProductionId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Stages_StageId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTimetables_TimeTables_PerformanceId",
                table: "UserTimetables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeTables",
                table: "TimeTables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                table: "Programs");

            migrationBuilder.RenameTable(
                name: "TimeTables",
                newName: "Performances");

            migrationBuilder.RenameTable(
                name: "Programs",
                newName: "Productions");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_StageId",
                table: "Performances",
                newName: "IX_Performances_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_TimeTables_ProductionId",
                table: "Performances",
                newName: "IX_Performances_ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Performances",
                table: "Performances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productions",
                table: "Productions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduction_Productions_ProductionsId",
                table: "GenreProduction",
                column: "ProductionsId",
                principalTable: "Productions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Productions_ProductionId",
                table: "Performances",
                column: "ProductionId",
                principalTable: "Productions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTimetables_Performances_PerformanceId",
                table: "UserTimetables",
                column: "PerformanceId",
                principalTable: "Performances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduction_Productions_ProductionsId",
                table: "GenreProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Productions_ProductionId",
                table: "Performances");

            migrationBuilder.DropForeignKey(
                name: "FK_Performances_Stages_StageId",
                table: "Performances");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTimetables_Performances_PerformanceId",
                table: "UserTimetables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productions",
                table: "Productions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Performances",
                table: "Performances");

            migrationBuilder.RenameTable(
                name: "Productions",
                newName: "Programs");

            migrationBuilder.RenameTable(
                name: "Performances",
                newName: "TimeTables");

            migrationBuilder.RenameIndex(
                name: "IX_Performances_StageId",
                table: "TimeTables",
                newName: "IX_TimeTables_StageId");

            migrationBuilder.RenameIndex(
                name: "IX_Performances_ProductionId",
                table: "TimeTables",
                newName: "IX_TimeTables_ProductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                table: "Programs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeTables",
                table: "TimeTables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduction_Programs_ProductionsId",
                table: "GenreProduction",
                column: "ProductionsId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Programs_ProductionId",
                table: "TimeTables",
                column: "ProductionId",
                principalTable: "Programs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Stages_StageId",
                table: "TimeTables",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTimetables_TimeTables_PerformanceId",
                table: "UserTimetables",
                column: "PerformanceId",
                principalTable: "TimeTables",
                principalColumn: "Id");
        }
    }
}
