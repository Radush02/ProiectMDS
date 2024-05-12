using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.Migrations
{
    /// <inheritdoc />
    public partial class imaginiAuto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nrImagini",
                table: "Postare",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nrImagini",
                table: "Postare");
        }
    }
}
