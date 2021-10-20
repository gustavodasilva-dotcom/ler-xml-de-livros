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
                    if (VerificarPasta())
                    {
                        await LerArquivo();
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private bool VerificarPasta()
        {
            try
            {
                var resultado = _pasta.VerificarPasta();

                if (resultado)
                {
                    _logger.LogInformation("Há arquivos na pasta XMLFiles");

                    return true;
                }

                _logger.LogInformation("Não há arquivos na pasta XMLFiles");

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task LerArquivo()
        {
            try
            {
                await _pasta.LerArquivo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
