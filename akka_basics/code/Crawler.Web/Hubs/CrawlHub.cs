using System;
using Akka.Actor;
using Crawler.Web.Actors;
using Microsoft.AspNetCore.SignalR;

namespace Crawler.Web.Hubs
{
    public class CrawlHub : Hub
    {

        public void StartCrawl(string message)
        {
            SystemActors.SignalRActor.Tell(message, ActorRefs.Nobody);
        }
    }
}