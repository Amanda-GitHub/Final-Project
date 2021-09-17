using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_CLOUD_45_2021.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
      
        [Display(Name ="Nome Comum")]
        public string NomeComum { get; set; }

        [Display(Name = "Nome Científico")]
        public string NomeCientífico { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Display(Name = "Preço")]
        public float Preco { get; set; }
        public string Foto { get; set; } //Url blob storage

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Encomenda> Encomendas { get; set; }








    }
}
