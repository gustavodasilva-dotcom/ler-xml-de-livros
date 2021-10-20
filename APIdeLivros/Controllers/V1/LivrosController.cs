using APIdeLivros.Exceptions;
using APIdeLivros.Models.InputModels;
using APIdeLivros.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APIdeLivros.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public async Task<ActionResult> Inserir([FromBody] LivroInputModel livro)
        {
            try
            {
                await _livroService.Inserir(livro);

                return Created("Livro cadastrado com sucesso!", livro);
            }
            catch(ConflictException)
            {
                return Conflict();
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Ocorreu o seguinte erro ao realizar esse processo: {e.Message}");
            }
        }
    }
}
