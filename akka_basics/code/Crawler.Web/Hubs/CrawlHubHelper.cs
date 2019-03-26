using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Crawler.Web.Actors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Crawler.Web.Hubs
{
    public class CrawlHubHelper : IHostedService
    {
        private readonly IHubContext<CrawlHub> _hub;

        public CrawlHubHelper(IHubContext<CrawlHub> hub)
        {
            _hub = hub;
        }

        internal void WriteMessage(string message)
        {
            _hub.Clients.All.SendAsync("writeStatus", message);
        }

        internal void SendLinks(string domain, List<string> links, bool isInternal)
        {
            _hub.Clients.All.SendAsync("setLinks", domain, links, isInternal);
        }

        internal void SendStatus(string domain, SignalRActor.DomainStatus status)
        {
            _hub.Clients.All.SendAsync("setStatus", domain, status.ToString());
        }

        internal void SendError(string domain, string error)
        {
            _hub.Clients.All.SendAsync("setError", domain, error);
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            AkkaStartupTasks.StartAkka();
            SystemActors.SignalRActor.Tell(new SignalRActor.SetHub(this), ActorRefs.NoSender);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}