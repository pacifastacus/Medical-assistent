using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace ServerAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddMvc())
                .Configure(app => app.UseMvc())
                .UseUrls("http://*:8080")
                .Build()
                .Run();
        }
    }
}
