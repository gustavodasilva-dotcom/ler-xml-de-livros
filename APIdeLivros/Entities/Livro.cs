using System;

namespace APIdeLivros.Entities
{
    public class Livro
    {
        public string Autor { get; set; }

        public string Titulo { get; set; }

        public string Genero { get; set; }

        public string Preco { get; set; }

        public DateTime DataPublicacao { get; set; }

        public string Descricao { get; set; }
    }
}
