using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMDS.Migrations
{
    /// <inheritdoc />
    public partial class First_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nrTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carteIdentitate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    permis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    numar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataExpirare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvv = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Card_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postare",
                columns: table => new
                {
                    PostareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pret = table.Column<int>(type: "int", nullable: false),
                    firma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kilometraj = table.Column<int>(type: "int", nullable: false),
                    anFabricatie = table.Column<int>(type: "int", nullable: false),
                    talon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carteIdentitateMasina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    asigurare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postare", x => x.PostareId);
                    table.ForeignKey(
                        name: "FK_Postare_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chirie",
                columns: table => new
                {
                    ChirieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostareId = table.Column<int>(type: "int", nullable: false),
                    dataStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataStop = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chirie", x => new { x.ChirieId, x.PostareId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Chirie_Postare_PostareId",
                        column: x => x.PostareId,
                        principalTable: "Postare",
                        principalColumn: "PostareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chirie_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    PostareId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comentariu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    dataReview = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => new { x.ReviewId, x.PostareId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Review_Postare_PostareId",
                        column: x => x.PostareId,
                        principalTable: "Postare",
                        principalColumn: "PostareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserId",
                table: "Card",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Chirie_PostareId",
                table: "Chirie",
                column: "PostareId");

            migrationBuilder.CreateIndex(
                name: "IX_Chirie_UserId",
                table: "Chirie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Postare_UserId",
                table: "Postare",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_PostareId",
                table: "Review",
                column: "PostareId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Chirie");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Postare");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
