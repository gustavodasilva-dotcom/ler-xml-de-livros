using LerXML.Business;
using LerXML.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LerXML
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddSingleton<IPastaBO, PastaBO>();
                    services.AddSingleton<IPastaRepository, PastaRepository>();
                });
    }
}