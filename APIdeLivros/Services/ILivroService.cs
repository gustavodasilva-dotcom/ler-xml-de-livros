using APIdeLivros.Models.InputModels;
using System.Threading.Tasks;

namespace APIdeLivros.Services
{
    public interface ILivroService
    {
        Task Inserir(LivroInputModel livro);
    }
}
