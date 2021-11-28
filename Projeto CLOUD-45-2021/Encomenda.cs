using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021.Models
{
    public class Encomenda
    {
        [Key]
        public int EncomendaId { get; set; }

        [Display(Name = "Data da Encomenda")]
        public DateTime DataEncomenda { get; set; } 
        
        public int Quantidade { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }

        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public ICollection<Fatura> Faturas { get; set; }

        public ICollection<Item_Encomenda> Itens_Encomenda { get; set; }

    }
}
