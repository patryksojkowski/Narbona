using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Narbona.Migrations
{
    /// <inheritdoc />
    public partial class PersonEmailFKeyRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_People_PersonId",
                table: "Email");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "People",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Email",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_People_PersonId",
                table: "Email",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_People_PersonId",
                table: "Email");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "People",
                newName: "Lastname");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Email",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_People_PersonId",
                table: "Email",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
