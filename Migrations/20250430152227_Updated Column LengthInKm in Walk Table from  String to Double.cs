using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nzWalksApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColumnLengthInKminWalkTablefromStringtoDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LengthInKm",
                table: "walks",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LengthInKm",
                table: "walks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
