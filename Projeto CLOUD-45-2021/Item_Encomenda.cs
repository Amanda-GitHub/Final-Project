using Projeto_CLOUD_45_2021.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021.Models
{
    public class Item_Encomenda
    {
        [Key]
        public int Item_EncomendaId { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [ForeignKey("Encomenda")]
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }

        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }
}
