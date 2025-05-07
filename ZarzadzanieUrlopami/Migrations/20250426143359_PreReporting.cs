using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZarzadzanieUrlopami.Migrations
{
    /// <inheritdoc />
    public partial class PreReporting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pracownicy_Role",
                table: "Pracownicy");

            migrationBuilder.DropIndex(
                name: "UQ_Pracownicy_Mail",
                table: "Pracownicy");

            migrationBuilder.DropIndex(
                name: "UQ_Pracownicy_Telefon",
                table: "Pracownicy");

            migrationBuilder.AlterColumn<string>(
                name: "telefon",
                table: "Pracownicy",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nazwisko",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mail",
                table: "Pracownicy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imie",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_roli",
                table: "Pracownicy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "haslo",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "data_rozpoczecia",
                table: "Pracownicy",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Pracownicy_Mail",
                table: "Pracownicy",
                column: "mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Pracownicy_Telefon",
                table: "Pracownicy",
                column: "telefon",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pracownicy_Role",
                table: "Pracownicy",
                column: "id_roli",
                principalTable: "Role",
                principalColumn: "id_roli",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pracownicy_Role",
                table: "Pracownicy");

            migrationBuilder.DropIndex(
                name: "UQ_Pracownicy_Mail",
                table: "Pracownicy");

            migrationBuilder.DropIndex(
                name: "UQ_Pracownicy_Telefon",
                table: "Pracownicy");

            migrationBuilder.AlterColumn<string>(
                name: "telefon",
                table: "Pracownicy",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "nazwisko",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "mail",
                table: "Pracownicy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "imie",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "id_roli",
                table: "Pracownicy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "haslo",
                table: "Pracownicy",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "data_rozpoczecia",
                table: "Pracownicy",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Pracownicy_Role",
                table: "Pracownicy",
                column: "id_roli",
                principalTable: "Role",
                principalColumn: "id_roli");
        }
    }
}
