using System.Threading.Tasks;

namespace LerXML.Business
{
    public interface IPastaBO
    {
        Task<bool> VerificarPasta();

        Task LerArquivo(bool retorno);
    }
}
