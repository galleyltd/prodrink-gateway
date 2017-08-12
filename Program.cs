using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace prodrink.gateway
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}