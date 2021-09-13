using LerXML.Entities;
using LerXML.Repository;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace LerXML.Business
{
    public class PastaBO : IPastaBO
    {
        private readonly string arquivo;

        private readonly IPastaRepository _pastaRepository;

        public PastaBO(IPastaRepository pastaRepository)
        {
            arquivo = @"C:\XMLFiles\books.xml";

            _pastaRepository = pastaRepository;
        }

        public async Task<bool> VerificarPasta()
        {
            if (File.Exists(arquivo))
            {
                return true;
            }

            return false;
        }

        public async Task LerArquivo(bool retorno)
        {
            using (XmlReader xml = XmlReader.Create(arquivo))
            {
                string autor = "", titulo = "", genero = "", preco = "", dataPublicacao = "", descricao = "";

                while (xml.Read())
                {
                    if (xml.NodeType == XmlNodeType.Element && xml.Name == "author")
                    {
                        autor = xml.ReadElementContentAsString();
                    }
                    else if (xml.NodeType == XmlNodeType.Element && xml.Name == "title")
                    {
                        titulo = xml.ReadElementContentAsString();
                    }
                    else if (xml.NodeType == XmlNodeType.Element && xml.Name == "genre")
                    {
                        genero = xml.ReadElementContentAsString();
                    }
                    else if (xml.NodeType == XmlNodeType.Element && xml.Name == "price")
                    {
                        preco = xml.ReadElementContentAsString();
                    }
                    else if (xml.NodeType == XmlNodeType.Element && xml.Name == "publish_date")
                    {
                        dataPublicacao = xml.ReadElementContentAsString();
                    }
                    else if (xml.NodeType == XmlNodeType.Element && xml.Name == "description")
                    {
                        descricao = xml.ReadElementContentAsString();
                    }

                    if (
                        !string.IsNullOrEmpty(autor) && !string.IsNullOrEmpty(titulo) &&
                        !string.IsNullOrEmpty(genero) && !string.IsNullOrEmpty(preco) &&
                        !string.IsNullOrEmpty(dataPublicacao) && !string.IsNullOrEmpty(descricao)
                        )
                    {
                        Livro livro = new Livro
                        {
                            Autor = autor,
                            Titulo = titulo,
                            Genero = genero,
                            Preco = preco,
                            DataPublicacao = dataPublicacao,
                            Descricao = descricao
                        };

                        await InserirLivro(livro);

                        autor = null;
                        titulo = null;
                        genero = null;
                        preco = null;
                        dataPublicacao = null;
                        descricao = null;
                    }
                }
            }
        }

        private async Task InserirLivro(Livro livro)
        {
            await _pastaRepository.InserirLivro(livro);
        }
    }
}
