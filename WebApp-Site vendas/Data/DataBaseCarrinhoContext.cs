using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Site_vendas.Models;

namespace WebApp_Site_vendas.Data
{
    public class DataBaseCarrinhoContext: DbContext
    {
        public DataBaseCarrinhoContext(DbContextOptions<DataBaseCarrinhoContext> options)
            : base(options)
        { }        

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<CarrinhoCompras> Carrinho { get; set; }

    }
}
