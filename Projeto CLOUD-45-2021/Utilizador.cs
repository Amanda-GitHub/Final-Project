using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Projeto_CLOUD_45_2021.Models
{
    public class Utilizador
    {
        [Key]
        public int UtilizadorId { get; set; }

        public string Nome { get; set; }        
        
        public int Contribuinte { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }        
       
        public string Telefone { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email com formato inválido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Morada { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }
        public string? Andar { get; set; }
        public string Localidade { get; set; }

        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        public ICollection<Encomenda> Encomendas { get; set; }

        public ICollection<Fatura> Faturas { get; set; }
        
        

    }
}
