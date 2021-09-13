using System.Collections.Generic;
using System.Threading.Tasks;

namespace LerXML.Business
{
    public interface IPastaBO
    {
        bool VerificarPasta();

        Task LerArquivo();
    }
}
