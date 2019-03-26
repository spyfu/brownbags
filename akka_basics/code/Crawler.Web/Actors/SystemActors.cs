using Akka.Actor;

namespace Crawler.Web.Actors
{
    public static class SystemActors
    {
        public static ActorSystem ActorSystem;
        public static IActorRef SignalRActor = ActorRefs.Nobody;
        public static IActorRef Crawler = ActorRefs.Nobody;
    }
}