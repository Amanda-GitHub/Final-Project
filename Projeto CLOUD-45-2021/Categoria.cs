﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public string Nome { get; set; }
        public bool? Interior { get; set; }
        public bool? Exterior { get; set; }        

        public ICollection<Produto> Produtos { get; set; }
    }
}
