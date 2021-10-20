using APIdeLivros.Business;
using APIdeLivros.Exceptions;
using APIdeLivros.Models.InputModels;
using System;
using System.Threading.Tasks;

namespace APIdeLivros.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroBusiness _livroBusiness;

        public LivroService(ILivroBusiness livroBusiness)
        {
            _livroBusiness = livroBusiness;
        }

        public async Task Inserir(LivroInputModel livro)
        {
            try
            {
                var retorno = await _livroBusiness.ValidarEntrada(livro);

                if (retorno != null)
                    throw new ConflictException();

                await _livroBusiness.InserirLivro(livro);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
