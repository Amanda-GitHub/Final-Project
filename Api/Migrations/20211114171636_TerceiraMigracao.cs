using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class TerceiraMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itens_Encomenda",
                columns: table => new
                {
                    Item_EncomendaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens_Encomenda", x => x.Item_EncomendaId);
                    table.ForeignKey(
                        name: "FK_Itens_Encomenda_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "EncomendaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itens_Encomenda_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1,
                column: "Foto",
                value: "https://asprojeto.blob.core.windows.net/amandaimages/plantas%20exterior/Verbena-600x600.jpg");

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 1,
                column: "Password",
                value: "Amanda@123");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_Encomenda_EncomendaId",
                table: "Itens_Encomenda",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_Encomenda_ProdutoId",
                table: "Itens_Encomenda",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens_Encomenda");

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1,
                column: "Foto",
                value: "");

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 1,
                column: "Password",
                value: "123@123");
        }
    }
}
