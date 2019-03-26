export const actorAndProps = `
    public static void SpinUpActor()
    {
        var SignalRProps = Props.Create<SignalRActor>(); 
        IActorRef SignalRActor = ActorSystem.ActorOf(SignalRProps, "signalr");
    }
`;