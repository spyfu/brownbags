using System;
using System.Collections.Generic;
using Akka.Actor;
using Crawler.Web.Hubs;

namespace Crawler.Web.Actors
{
    public class SignalRActor : ReceiveActor, IWithUnboundedStash
    {
        public IStash Stash { get; set; }
        private CrawlHubHelper _hub;
        
        #region Messages
        
        public class SetHub : INoSerializationVerificationNeeded
        {
            public SetHub(CrawlHubHelper hub)
            {
                Hub = hub;
            }
            public CrawlHubHelper Hub { get; }
        }

        public class LinksFound
        {
            public string Domain { get; }
            public List<string> InternalLinks { get; }
            public List<string> ExternalLinks { get; }

            public LinksFound(string domain, List<string> internalLinks, List<string> externalLinks)
            {
                (Domain, InternalLinks, ExternalLinks) = (domain, internalLinks, externalLinks);
            }
        }

        public enum DomainStatus
        {
            ToDo,
            InProgress,
            Done,
            Error,
        }

        public class SetStatus
        {
            public string Domain { get; }
            public DomainStatus Status { get; }

            public SetStatus(string domain, DomainStatus status)
            {
                (Domain, Status) = (domain, status);
            }
        }

        public class Errored
        {
            public string Domain { get; }
            public string Error { get; }

            public Errored(string domain, string error)
            {
                (Domain, Error) = (domain, error);
            }
        }
        
        #endregion

        public SignalRActor()
        {
            WaitingForHub();
        }

        private void WaitingForHub()
        {
            Receive<SetHub>(msg =>
            {
                _hub = msg.Hub;
                Become(HubAvailable);
                Stash.UnstashAll();
            });
            
            ReceiveAny(_ => Stash.Stash());
        }

        private void HubAvailable()
        {
            Receive<string>(msg =>
            {
                _hub.WriteMessage($"received: {msg}");
                SystemActors.Crawler.Tell(new Crawler.StartCrawl(msg));
            });

            Receive<LinksFound>(msg =>
            {
               _hub.SendLinks(msg.Domain, msg.InternalLinks, true); 
               _hub.SendLinks(msg.Domain, msg.ExternalLinks, false);
            });

            Receive<SetStatus>(msg => _hub.SendStatus(msg.Domain, msg.Status));

            Receive<Errored>(msg => _hub.SendError(msg.Domain, msg.Error));
        }
    }
}