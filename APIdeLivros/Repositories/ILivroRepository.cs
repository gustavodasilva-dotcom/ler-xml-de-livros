using APIdeLivros.Entities;
using APIdeLivros.Models.InputModels;
using System.Threading.Tasks;

namespace APIdeLivros.Repositories
{
    public interface ILivroRepository
    {
        Task<bool> LivroExiste(LivroInputModel livro);

        Task GravarLog(Retorno retorno, string json);

        Task GravarLog(string mensagem, string json);

        Task InserirLivro(Livro livro);
    }
}
