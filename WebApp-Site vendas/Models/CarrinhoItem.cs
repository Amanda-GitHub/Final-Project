using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Site_vendas.Models
{
    public class CarrinhoItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CarrinhoId { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
    }
}
