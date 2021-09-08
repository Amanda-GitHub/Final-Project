using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021
{
    public class Fatura
    {
        public int FaturaId { get; set; }

        [Display(Name = "Número")]
        public int NumeroFatura { get; set; }

        [Display(Name = "Data de Emissão")]
        public DateTime DataEmissao { get; set; }
    }
}
