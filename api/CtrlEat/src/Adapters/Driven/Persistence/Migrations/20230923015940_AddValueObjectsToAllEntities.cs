using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddValueObjectsToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_client_DocumentId",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_Email",
                table: "client");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "client",
                newName: "FullName_LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "client",
                newName: "FullName_FirstName");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "client",
                newName: "PersonalDocument_DocumentType");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "client",
                newName: "PersonalDocument_DocumentId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "client",
                newName: "Email_Address");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalDocument_DocumentType",
                table: "client",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_client_Email_Address",
                table: "client",
                column: "Email_Address",
                unique: true,
                filter: "[Email_Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_client_PersonalDocument_DocumentId",
                table: "client",
                column: "PersonalDocument_DocumentId",
                unique: true,
                filter: "[PersonalDocument_DocumentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_client_Email_Address",
                table: "client");

            migrationBuilder.DropIndex(
                name: "IX_client_PersonalDocument_DocumentId",
                table: "client");

            migrationBuilder.RenameColumn(
                name: "PersonalDocument_DocumentType",
                table: "client",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "PersonalDocument_DocumentId",
                table: "client",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "FullName_LastName",
                table: "client",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName_FirstName",
                table: "client",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Email_Address",
                table: "client",
                newName: "Email");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentType",
                table: "client",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_DocumentId",
                table: "client",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_client_Email",
                table: "client",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }
    }
}
