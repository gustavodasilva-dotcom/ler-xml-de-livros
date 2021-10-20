using LerXML.Entities;
using System.Threading.Tasks;

namespace LerXML.Connections
{
    public interface IPastaConnection
    {
        Task<Retorno> InserirLivro(Livro livro);
    }
}
