using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veebipood.Migrations
{
    /// <inheritdoc />
    public partial class AddedToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aadressid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tanav = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postiindeks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aadressid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategooriad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimetus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategooriad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kontaktandmed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontaktandmed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maksestaatused",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Makstud = table.Column<bool>(type: "bit", nullable: false),
                    Maksetahtaeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MakstudSumma = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaksmiseKuupaev = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maksestaatused", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tooted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimetus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategooriaId = table.Column<int>(type: "int", nullable: false),
                    Hind = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PildiUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktiivne = table.Column<bool>(type: "bit", nullable: false),
                    Laokogus = table.Column<int>(type: "int", nullable: false),
                    Vananemisaeg = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tooted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tooted_Kategooriad_KategooriaId",
                        column: x => x.KategooriaId,
                        principalTable: "Kategooriad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kliendid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KontaktandmedId = table.Column<int>(type: "int", nullable: false),
                    AadressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kliendid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kliendid_Aadressid_AadressId",
                        column: x => x.AadressId,
                        principalTable: "Aadressid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kliendid_Kontaktandmed_KontaktandmedId",
                        column: x => x.KontaktandmedId,
                        principalTable: "Kontaktandmed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arved",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kuupaev = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kogusumma = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaksestaatusId = table.Column<int>(type: "int", nullable: false),
                    KlientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arved", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arved_Kliendid_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Kliendid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arved_Maksestaatused_MaksestaatusId",
                        column: x => x.MaksestaatusId,
                        principalTable: "Maksestaatused",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arveread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToodeId = table.Column<int>(type: "int", nullable: false),
                    Kogus = table.Column<int>(type: "int", nullable: false),
                    ArveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arveread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arveread_Arved_ArveId",
                        column: x => x.ArveId,
                        principalTable: "Arved",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arveread_Tooted_ToodeId",
                        column: x => x.ToodeId,
                        principalTable: "Tooted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arved_KlientId",
                table: "Arved",
                column: "KlientId");

            migrationBuilder.CreateIndex(
                name: "IX_Arved_MaksestaatusId",
                table: "Arved",
                column: "MaksestaatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Arveread_ArveId",
                table: "Arveread",
                column: "ArveId");

            migrationBuilder.CreateIndex(
                name: "IX_Arveread_ToodeId",
                table: "Arveread",
                column: "ToodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kliendid_AadressId",
                table: "Kliendid",
                column: "AadressId");

            migrationBuilder.CreateIndex(
                name: "IX_Kliendid_KontaktandmedId",
                table: "Kliendid",
                column: "KontaktandmedId");

            migrationBuilder.CreateIndex(
                name: "IX_Tooted_KategooriaId",
                table: "Tooted",
                column: "KategooriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arveread");

            migrationBuilder.DropTable(
                name: "Arved");

            migrationBuilder.DropTable(
                name: "Tooted");

            migrationBuilder.DropTable(
                name: "Kliendid");

            migrationBuilder.DropTable(
                name: "Maksestaatused");

            migrationBuilder.DropTable(
                name: "Kategooriad");

            migrationBuilder.DropTable(
                name: "Aadressid");

            migrationBuilder.DropTable(
                name: "Kontaktandmed");
        }
    }
}
