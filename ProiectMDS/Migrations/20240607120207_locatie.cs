using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.Migrations
{
    /// <inheritdoc />
    public partial class locatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "adresa_formala",
                table: "Postare",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "adresa_user",
                table: "Postare",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adresa_formala",
                table: "Postare");

            migrationBuilder.DropColumn(
                name: "adresa_user",
                table: "Postare");
        }
    }
}
