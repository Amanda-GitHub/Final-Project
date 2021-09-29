using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Site_vendas.Models
{
    public class CarrinhoCompras
    {
       [Key]
        public Guid CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal PrecoUnit { get; set; }

    }
    
}
