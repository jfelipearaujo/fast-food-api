using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedValueObject_Money : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "product",
                newName: "Price_Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Price_Currency",
                table: "product",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<decimal>(
                name: "Price_Amount",
                table: "product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Amount",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "product",
                newName: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "product",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "product",
                type: "decimal(4,2)",
                precision: 4,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
