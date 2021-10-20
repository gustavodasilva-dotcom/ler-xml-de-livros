using System;
using System.ComponentModel.DataAnnotations;

namespace APIdeLivros.Models.InputModels
{
    public class LivroInputModel
    {
        [Required]
        public string Autor { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public string Preco { get; set; }

        [Required]
        public DateTime DataPublicacao { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
