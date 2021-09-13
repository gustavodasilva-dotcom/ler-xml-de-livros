using LerXML.Entities;
using System.Threading.Tasks;

namespace LerXML.Repository
{
    public interface IPastaRepository
    {
        Task InserirLivro(Livro livro);
    }
}
