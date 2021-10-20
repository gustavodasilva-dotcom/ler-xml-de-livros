using APIdeLivros.Entities;
using APIdeLivros.Models.InputModels;
using System.Threading.Tasks;

namespace APIdeLivros.Business
{
    public interface ILivroBusiness
    {
        Task<Retorno> ValidarEntrada(LivroInputModel livro);

        Task InserirLivro(LivroInputModel livro);
    }
}
