using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021
{
    public class Encomenda
    {
        public int EncomendaId { get; set; }

        [Display(Name = "Data da Encomenda")]
        public DateTime DataEncomenda { get; set; } 
        
        public int Quantidade { get; set; }

        [Display(Name = "Valor Total")]
        public float ValorTotal { get; set; }

        [ForeignKey("Utilizador")]
        public Utilizador Utilizador { get; set; }

        [ForeignKey("Produto")]
        public Produto Produto { get; set; }


    }
}
