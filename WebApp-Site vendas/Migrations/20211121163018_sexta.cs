using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_Site_vendas.Migrations
{
    public partial class sexta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrinhoCompras",
                table: "CarrinhoCompras");

            migrationBuilder.RenameTable(
                name: "CarrinhoCompras",
                newName: "Carrinho");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho",
                column: "CarrinhoId");

            migrationBuilder.CreateTable(
                name: "CarrinhoItem",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarrinhoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItem", x => x.ItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrinho",
                table: "Carrinho");

            migrationBuilder.RenameTable(
                name: "Carrinho",
                newName: "CarrinhoCompras");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrinhoCompras",
                table: "CarrinhoCompras",
                column: "CarrinhoId");
        }
    }
}
