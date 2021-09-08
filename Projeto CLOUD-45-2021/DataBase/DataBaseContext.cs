using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021.DataBase
{
    public class DataBaseContext: DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }

    }

    
}
