using LerXML.Business;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LerXML
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IPastaBO _pasta;

        public Worker(ILogger<Worker> logger, IPastaBO pasta)
        {
            _logger = logger;

            _pasta = pasta;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var retorno = await VerificarPasta();

                    await LerArquivo(retorno);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task<bool> VerificarPasta()
        {
            string mensagem;
            
            var resultado = await _pasta.VerificarPasta();

            if (resultado)
            {
                mensagem = "Há arquivos na pasta XMLFiles";
            }
            else
            {
                mensagem = "Não há arquivos na pasta XMLFiles";
            }

            _logger.LogInformation(mensagem);

            return resultado;
        }

        private async Task LerArquivo(bool retorno)
        {
            await _pasta.LerArquivo(retorno);
        }
    }
}
