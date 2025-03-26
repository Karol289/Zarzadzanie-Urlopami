using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZarzadzanieUrlopami.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id_roli = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3D4847E12850301E", x => x.id_roli);
                });

            migrationBuilder.CreateTable(
                name: "Typy_Statusow",
                columns: table => new
                {
                    id_typu_stat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Typy_Sta__5301E33B60099E9B", x => x.id_typu_stat);
                });

            migrationBuilder.CreateTable(
                name: "Typy_Urlopow",
                columns: table => new
                {
                    id_typu_urlopu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Typy_Url__EC7B687853AF0A3D", x => x.id_typu_urlopu);
                });

            migrationBuilder.CreateTable(
                name: "Typy_Zwolnien",
                columns: table => new
                {
                    id_typu_zwolnienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Typy_Zwo__57ED5384C9C8D8FC", x => x.id_typu_zwolnienia);
                });

            migrationBuilder.CreateTable(
                name: "Wystawcy_Zwolnien",
                columns: table => new
                {
                    id_wystawcy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    adres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Wystawcy__27A7F6F0F0686277", x => x.id_wystawcy);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    id_pracownika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_roli = table.Column<int>(type: "int", nullable: true),
                    imie = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    nazwisko = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    plec = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    wiek = table.Column<int>(type: "int", nullable: true),
                    mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dzieci_ponizej_14 = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    data_rozpoczecia = table.Column<DateOnly>(type: "date", nullable: true),
                    staz_pracy_w_dniach = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    adres = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    haslo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pracowni__5D8E25F273BAF34A", x => x.id_pracownika);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Role",
                        column: x => x.id_roli,
                        principalTable: "Role",
                        principalColumn: "id_roli");
                });

            migrationBuilder.CreateTable(
                name: "Status_Urlopu",
                columns: table => new
                {
                    id_statusu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_typu_statusu = table.Column<int>(type: "int", nullable: true),
                    wyjaśnienie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Status_U__761651A744506549", x => x.id_statusu);
                    table.ForeignKey(
                        name: "FK_StatusUrlopu_TypyStatusow",
                        column: x => x.id_typu_statusu,
                        principalTable: "Typy_Statusow",
                        principalColumn: "id_typu_stat");
                });

            migrationBuilder.CreateTable(
                name: "Dni_robocze",
                columns: table => new
                {
                    id_dnia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_kierownika = table.Column<int>(type: "int", nullable: true),
                    data_dnia = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dni_robo__9DC8EE78B8C63145", x => x.id_dnia);
                    table.ForeignKey(
                        name: "FK_DniRobocze_Pracownicy",
                        column: x => x.id_kierownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Dostepne_Urlopy_Roczne",
                columns: table => new
                {
                    id_dostepnego_urlopu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pracownika = table.Column<int>(type: "int", nullable: true),
                    id_typu_urlopu = table.Column<int>(type: "int", nullable: true),
                    ilosc = table.Column<int>(type: "int", nullable: true),
                    rok = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(datepart(year,getdate()))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dostepne__6A75A46107D59F28", x => x.id_dostepnego_urlopu);
                    table.ForeignKey(
                        name: "FK_DostepneUrlopy_Pracownicy",
                        column: x => x.id_pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DostepneUrlopy_TypyUrlopow",
                        column: x => x.id_typu_urlopu,
                        principalTable: "Typy_Urlopow",
                        principalColumn: "id_typu_urlopu");
                });

            migrationBuilder.CreateTable(
                name: "Zwolnienia_Lekarskie",
                columns: table => new
                {
                    id_zwolnienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pracownika = table.Column<int>(type: "int", nullable: false),
                    id_wystawcy = table.Column<int>(type: "int", nullable: false),
                    id_typu = table.Column<int>(type: "int", nullable: false),
                    data_pocz = table.Column<DateOnly>(type: "date", nullable: true),
                    data_kon = table.Column<DateOnly>(type: "date", nullable: true),
                    opis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Zwolnien__290A808EC69EAA28", x => x.id_zwolnienia);
                    table.ForeignKey(
                        name: "FK_ZwolnieniaLekarskie_Pracownicy",
                        column: x => x.id_pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZwolnieniaLekarskie_TypyZwolnien",
                        column: x => x.id_typu,
                        principalTable: "Typy_Zwolnien",
                        principalColumn: "id_typu_zwolnienia");
                    table.ForeignKey(
                        name: "FK_ZwolnieniaLekarskie_WystawcyZwolnien",
                        column: x => x.id_wystawcy,
                        principalTable: "Wystawcy_Zwolnien",
                        principalColumn: "id_wystawcy");
                });

            migrationBuilder.CreateTable(
                name: "Urlopy",
                columns: table => new
                {
                    id_urlopu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pracownika = table.Column<int>(type: "int", nullable: false),
                    id_typu_urlopu = table.Column<int>(type: "int", nullable: false),
                    id_statusu = table.Column<int>(type: "int", nullable: false),
                    data_pocz = table.Column<DateOnly>(type: "date", nullable: true),
                    data_kon = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Urlopy__DE8DBE5775C813AF", x => x.id_urlopu);
                    table.ForeignKey(
                        name: "FK_Urlopy_Pracownicy",
                        column: x => x.id_pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Urlopy_Status_Urlopu",
                        column: x => x.id_statusu,
                        principalTable: "Status_Urlopu",
                        principalColumn: "id_statusu");
                    table.ForeignKey(
                        name: "FK_Urlopy_TypyUrlopow",
                        column: x => x.id_typu_urlopu,
                        principalTable: "Typy_Urlopow",
                        principalColumn: "id_typu_urlopu");
                });

            migrationBuilder.CreateTable(
                name: "Zmiany",
                columns: table => new
                {
                    id_zmiany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_dnia = table.Column<int>(type: "int", nullable: false),
                    id_pracownika = table.Column<int>(type: "int", nullable: false),
                    godz_rozp = table.Column<TimeOnly>(type: "time", nullable: true),
                    godz_zakon = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Zmiany__9B21969B31FEA3D7", x => x.id_zmiany);
                    table.ForeignKey(
                        name: "FK_Zmiany_DniRobocze",
                        column: x => x.id_dnia,
                        principalTable: "Dni_robocze",
                        principalColumn: "id_dnia");
                    table.ForeignKey(
                        name: "FK_Zmiany_Pracownicy",
                        column: x => x.id_pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika");
                });

            migrationBuilder.CreateTable(
                name: "Zastepstwa",
                columns: table => new
                {
                    id_zastepstwa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_zmiany = table.Column<int>(type: "int", nullable: false),
                    id_pracownika = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Zastepst__6C01E0AABCA220AD", x => x.id_zastepstwa);
                    table.ForeignKey(
                        name: "FK_Zastepstwa_Pracownicy",
                        column: x => x.id_pracownika,
                        principalTable: "Pracownicy",
                        principalColumn: "id_pracownika");
                    table.ForeignKey(
                        name: "FK_Zastepstwa_Zmiany",
                        column: x => x.id_zmiany,
                        principalTable: "Zmiany",
                        principalColumn: "id_zmiany");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dni_robocze_id_kierownika",
                table: "Dni_robocze",
                column: "id_kierownika");

            migrationBuilder.CreateIndex(
                name: "IX_DniRobocze_Dzien",
                table: "Dni_robocze",
                column: "data_dnia");

            migrationBuilder.CreateIndex(
                name: "IX_Dostepne_Urlopy_Roczne_id_pracownika",
                table: "Dostepne_Urlopy_Roczne",
                column: "id_pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Dostepne_Urlopy_Roczne_id_typu_urlopu",
                table: "Dostepne_Urlopy_Roczne",
                column: "id_typu_urlopu");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_id_roli",
                table: "Pracownicy",
                column: "id_roli");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_Nazwisko",
                table: "Pracownicy",
                column: "nazwisko");

            migrationBuilder.CreateIndex(
                name: "UQ_Pracownicy_Mail",
                table: "Pracownicy",
                column: "mail",
                unique: true,
                filter: "[mail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Pracownicy_Telefon",
                table: "Pracownicy",
                column: "telefon",
                unique: true,
                filter: "[telefon] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Urlopu_id_typu_statusu",
                table: "Status_Urlopu",
                column: "id_typu_statusu");

            migrationBuilder.CreateIndex(
                name: "IX_TypyStatusow_Nazwa",
                table: "Typy_Statusow",
                column: "nazwa");

            migrationBuilder.CreateIndex(
                name: "UQ_TypyStatusow_Nazwa",
                table: "Typy_Statusow",
                column: "nazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypyUrlopow_Nazwa",
                table: "Typy_Urlopow",
                column: "nazwa");

            migrationBuilder.CreateIndex(
                name: "UQ_TypyUrlopow_Nazwa",
                table: "Typy_Urlopow",
                column: "nazwa",
                unique: true,
                filter: "[nazwa] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TypyZwolnien_Nazwa",
                table: "Typy_Zwolnien",
                column: "nazwa");

            migrationBuilder.CreateIndex(
                name: "UQ_Typy_Zwolnien_Nazwa",
                table: "Typy_Zwolnien",
                column: "nazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urlopy_id_pracownika",
                table: "Urlopy",
                column: "id_pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Urlopy_id_statusu",
                table: "Urlopy",
                column: "id_statusu");

            migrationBuilder.CreateIndex(
                name: "IX_Urlopy_id_typu_urlopu",
                table: "Urlopy",
                column: "id_typu_urlopu");

            migrationBuilder.CreateIndex(
                name: "IX_WystawcyZwolnien_Nazwa",
                table: "Wystawcy_Zwolnien",
                column: "nazwa");

            migrationBuilder.CreateIndex(
                name: "UQ_WystawcyZwolnien_Nazwa",
                table: "Wystawcy_Zwolnien",
                column: "nazwa",
                unique: true,
                filter: "[nazwa] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Zastepstwa_id_pracownika",
                table: "Zastepstwa",
                column: "id_pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Zastepstwa_id_zmiany",
                table: "Zastepstwa",
                column: "id_zmiany");

            migrationBuilder.CreateIndex(
                name: "IX_Zmiany_id_dnia",
                table: "Zmiany",
                column: "id_dnia");

            migrationBuilder.CreateIndex(
                name: "IX_Zmiany_id_pracownika",
                table: "Zmiany",
                column: "id_pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Zwolnienia_Lekarskie_id_pracownika",
                table: "Zwolnienia_Lekarskie",
                column: "id_pracownika");

            migrationBuilder.CreateIndex(
                name: "IX_Zwolnienia_Lekarskie_id_typu",
                table: "Zwolnienia_Lekarskie",
                column: "id_typu");

            migrationBuilder.CreateIndex(
                name: "IX_Zwolnienia_Lekarskie_id_wystawcy",
                table: "Zwolnienia_Lekarskie",
                column: "id_wystawcy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dostepne_Urlopy_Roczne");

            migrationBuilder.DropTable(
                name: "Urlopy");

            migrationBuilder.DropTable(
                name: "Zastepstwa");

            migrationBuilder.DropTable(
                name: "Zwolnienia_Lekarskie");

            migrationBuilder.DropTable(
                name: "Status_Urlopu");

            migrationBuilder.DropTable(
                name: "Typy_Urlopow");

            migrationBuilder.DropTable(
                name: "Zmiany");

            migrationBuilder.DropTable(
                name: "Typy_Zwolnien");

            migrationBuilder.DropTable(
                name: "Wystawcy_Zwolnien");

            migrationBuilder.DropTable(
                name: "Typy_Statusow");

            migrationBuilder.DropTable(
                name: "Dni_robocze");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
