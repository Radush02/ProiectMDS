using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.Migrations
{
    /// <inheritdoc />
    public partial class k : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Support",
                table: "Support");

            migrationBuilder.DropColumn(
                               name: "SupportId",
                                              table: "Support");

                migrationBuilder.AddColumn<int>(
                    name: "SupportId",
                    table: "Support",
                    type: "int",
                    nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "dummyId",
                table: "Support",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Support",
                table: "Support",
                column: "dummyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Support",
                table: "Support");

            migrationBuilder.DropColumn(
                name: "dummyId",
                table: "Support");

            migrationBuilder.AlterColumn<int>(
                name: "SupportId",
                table: "Support",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Support",
                table: "Support",
                column: "SupportId");
        }
    }
}
