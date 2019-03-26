using System.IO;
using Akka.Actor;
using Akka.Configuration;
using Crawler.Web.Actors;

namespace Crawler.Web
{
    public class AkkaStartupTasks
    {
        public static ActorSystem StartAkka()
        {
            SystemActors.ActorSystem = ActorSystem.Create("webcrawler");
            SystemActors.SignalRActor = SystemActors.ActorSystem.ActorOf(Props.Create<SignalRActor>(), "signalr");
            SystemActors.Crawler = SystemActors.ActorSystem.ActorOf(Props.Create<Actors.Crawler>(), "crawler");

            return SystemActors.ActorSystem;
        }
    }
}