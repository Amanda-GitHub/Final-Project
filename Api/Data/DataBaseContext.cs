using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    public class DataBaseContext: IdentityDbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Item_Encomenda> Itens_Encomenda { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Utilizador>().HasData(
                new Utilizador() { UtilizadorId = 1, Nome = "Amanda Nunes", Contribuinte = 123456789, DataNascimento = new DateTime(1986, 2, 7), Telefone = "212212212", Email = "amanda@gmail.com",  Password = "Amanda@123", Morada = "Rua da Judiaria", Numero = 21, Andar = "2º esq", Localidade = "Lisboa", CodigoPostal = "2800-000"});

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria() {CategoriaId = 1, Nome = "Planta Exterior"},
                new Categoria() {CategoriaId = 2, Nome = "Planta Interior"});

            modelBuilder.Entity<Produto>().HasData(
                new Produto() {ProdutoId = 1, NomeComum = "Verbena", NomeCientífico = "Verbena bonariensis", Descricao = "Planta herbácea perene, originária da América do Sul mas que atualmente é subespontânea no nosso território. Quando cultivada como ornamental e em condições ideais, a Verbena bonariensis pode crescer até 1 metro de altura. As suas atrativas flores preenchem os canteiros de coloração roxa durante o verão e parte do Outono.", Preco = 12, CategoriaId = 2, Foto = "https://asprojeto.blob.core.windows.net/amandaimages/plantas%20exterior/Verbena-600x600.jpg" });            
        }        

      
    }

    
}
