using APIdeLivros.Entities;
using APIdeLivros.Models.InputModels;
using APIdeLivros.Repositories;
using System;
using System.Threading.Tasks;

namespace APIdeLivros.Business
{
    public class LivroBusiness : ILivroBusiness
    {
        private readonly ILivroRepository _livroRepository;

        private readonly IJsonBusiness _jsonBusiness;

        public LivroBusiness(ILivroRepository livroRepository, IJsonBusiness jsonBusiness)
        {
            _livroRepository = livroRepository;

            _jsonBusiness = jsonBusiness;
        }

        public async Task<Retorno> ValidarEntrada(LivroInputModel livro)
        {
            Retorno retorno = null;

            try
            {
                var json = _jsonBusiness.ConverterModelParaJson(livro);

                if (await _livroRepository.LivroExiste(livro))
                {
                    retorno = new Retorno
                    {
                        StatusCode = 409,
                        Mensagem = "Existe um livro cadastrado com esse nome."
                    };

                    await _livroRepository.GravarLog(retorno, json);
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InserirLivro(LivroInputModel livro)
        {
            try
            {
                var json = _jsonBusiness.ConverterModelParaJson(livro);

                var livroInput = new Livro
                {
                    Autor = livro.Autor,
                    Titulo = livro.Titulo,
                    Genero = livro.Genero,
                    Preco = livro.Preco,
                    DataPublicacao = livro.DataPublicacao,
                    Descricao = livro.Descricao
                };

                await _livroRepository.InserirLivro(livroInput);

                var mensagem = "Livro cadastrado com sucesso!";

                await _livroRepository.GravarLog(mensagem, json);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
