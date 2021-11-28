using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_Site_vendas.Migrations
{
    public partial class quinta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
