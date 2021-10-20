using LerXML.Entities;
using LerXML.Connections;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace LerXML.Business
{
    public class PastaBO : IPastaBO
    {
        private readonly string caminhoArquivo;

        private readonly string caminhoArquivoProcessado;

        private readonly IPastaConnection _pastaConnection;

        public PastaBO(IPastaConnection pastaConnection)
        {
            caminhoArquivo = @"C:\XMLFiles\";

            caminhoArquivoProcessado = @"C:\XMLFiles\Processados\";

            _pastaConnection = pastaConnection;
        }

        public bool VerificarPasta()
        {
            foreach (string arquivo in Directory.GetFiles(caminhoArquivo, "*.xml"))
            {
                if (File.Exists(arquivo))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task LerArquivo()
        {
            foreach (string arquivo in Directory.GetFiles(caminhoArquivo, "*.xml"))
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

                            descricao = descricao.Replace("\n", string.Empty);
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

                MoverArquivoParaProcessados(arquivo);
            }
        }

        private async Task InserirLivro(Livro livro)
        {
            try
            {
                var response = await _pastaConnection.InserirLivro(livro);

                Console.WriteLine("Retorno da integração: {0}", response.StatusCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MoverArquivoParaProcessados(string arquivo)
        {
            string nomeOrigemArquivo = arquivo.Split("\\").GetValue(2).ToString();

            var nomeArquivo = nomeOrigemArquivo + "." + Convert.ToString(DateTime.Now.ToString("yyyy''MM''dd'T'HH''mm''ss"));

            File.Move(arquivo, Path.Combine(caminhoArquivoProcessado, nomeArquivo));
        }
    }
}
