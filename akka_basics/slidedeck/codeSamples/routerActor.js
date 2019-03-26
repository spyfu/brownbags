export const routerActor = `
public class RouterActor : ReceiveActor
{
    private IActorRef _workers;

    public RouterActor()
    {
        _workers = Context.ActorOf(
            Props.Create<Worker>().WithRouter(new RoundRobinPool(5)),
            "routerWorkerPool"
        );

        Receive<Job>(job => _workers.Forward(job));
    }
}
`;