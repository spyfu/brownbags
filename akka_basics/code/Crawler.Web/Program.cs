using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IO;
using Crawler.Web.Actors;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Crawler.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            Console.CancelKeyPress += async (_, __) =>
            {
                var wait = CoordinatedShutdown.Get(SystemActors.ActorSystem).Run();
                await host.StopAsync();
                await wait;
            };

            host.Run();

            SystemActors.ActorSystem?.WhenTerminated.Wait();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
