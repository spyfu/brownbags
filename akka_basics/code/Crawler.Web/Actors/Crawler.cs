using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Akka.Actor;
using System.Threading.Tasks;
using Akka.Util.Internal;
using HtmlAgilityPack;

namespace Crawler.Web.Actors
{
    public class Crawler : ReceiveActor
    {
        #region Messages

        public class StartCrawl
        {
            public string Uri { get; }

            public StartCrawl(string uri)
            {
                Uri = uri;
            }
        }
        
        #endregion
        
        public Crawler()
        {
            ReceiveAsync<StartCrawl>(StartCrawlHandler);
        }

        private async Task StartCrawlHandler(StartCrawl message)
        {
            try
            {
                // let frontend know the crawl has started
                SystemActors.SignalRActor.Tell(new SignalRActor.SetStatus(message.Uri, SignalRActor.DomainStatus.InProgress));
                
                // validate uri (slash get full uri)
                Console.WriteLine($"starting {message.Uri}");
                var builder = new UriBuilder();
                builder.Host = message.Uri;
                builder.Scheme = "https";
                var uri = builder.Uri;
                
                // fire off request
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();
                var page = new HtmlDocument();
                page.LoadHtml(content);
                var links = page.DocumentNode.Descendants("a"); // all anchor links
                
                Console.WriteLine($"{ links.Count() } links found");
                
                // get internal / external links
                var internalLinks = new List<string>();
                var externalLinks = new List<string>();
                
                links.ForEach(link =>
                {
                    var href = link.Attributes["href"].Value;
                    Console.WriteLine($"link: {href}");

                    if (href[0] == '/')
                    {
                        internalLinks.Add(href);
                    }
                    else if (href[0] == 'h')
                    {
                        externalLinks.Add(href);
                    }
                });

                internalLinks = internalLinks.Distinct().ToList();
                externalLinks = externalLinks.Distinct().ToList();
                
                // let frontend know we've got links
                SystemActors.SignalRActor.Tell(new SignalRActor.LinksFound(message.Uri, internalLinks, externalLinks));
                
                // update status
                SystemActors.SignalRActor.Tell(new SignalRActor.SetStatus(message.Uri, SignalRActor.DomainStatus.Done));
            }
            catch (Exception e)
            {
                SystemActors.SignalRActor.Tell(new SignalRActor.Errored(message.Uri, e.ToString()));
            }
        }
    }
}