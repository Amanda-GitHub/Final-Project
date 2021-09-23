using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class PrimeiraAlteração : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataRegisto",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Categorias");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1,
                column: "Nome",
                value: "Planta Exterior");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2,
                column: "Nome",
                value: "Planta Interior");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegisto",
                table: "Utilizadores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Plantas", "Interior" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2,
                columns: new[] { "Nome", "Tipo" },
                values: new object[] { "Plantas", "Interior" });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 1,
                column: "DataRegisto",
                value: new DateTime(2021, 9, 19, 19, 2, 23, 920, DateTimeKind.Local).AddTicks(5540));
        }
    }
}
