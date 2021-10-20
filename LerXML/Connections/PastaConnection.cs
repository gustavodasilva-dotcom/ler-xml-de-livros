using LerXML.Entities;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace LerXML.Connections
{
    public class PastaConnection : IPastaConnection
    {
        public async Task<Retorno> InserirLivro(Livro livro)
        {
            try
            {
                var client = new RestClient("https://localhost:44302/api/V1/Livros");

                client.Timeout = -1;

                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");

                var body = @"{
                " + "\n" +
                                $@"    ""Autor"": ""{livro.Autor}"",
                " + "\n" +
                                $@"    ""Titulo"": ""{livro.Titulo}"",
                " + "\n" +
                                $@"    ""Genero"": ""{livro.Genero}"",
                " + "\n" +
                                $@"    ""Preco"": ""{livro.Preco}"",
                " + "\n" +
                                $@"    ""DataPublicacao"": ""{livro.DataPublicacao}"",
                " + "\n" +
                                $@"    ""Descricao"": ""{livro.Descricao}""
                " + "\n" +
                                @"}
                " + "\n" +
                @"";

                request.AddParameter("application/json", body, ParameterType.RequestBody);

                IRestResponse response = await client.ExecuteAsync(request);

                return new Retorno
                {
                    StatusCode = (int)response.StatusCode,
                    Mensagem = response.Content
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
