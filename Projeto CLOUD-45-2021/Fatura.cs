﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021
{
    public class Fatura
    {
        [Key]
        [Display(Name = "Número")]
        public int FaturaId { get; set; }      
        

        [Display(Name = "Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }

        [ForeignKey("Encomenda")]
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }

        [Display(Name = "Valor Total")]
        public float ValorTotal { get; set; }
    }
}
