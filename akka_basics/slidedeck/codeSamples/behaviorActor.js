export const behaviorActor = `
public class WaitForAvail : ReceiveActor,IWithUnboundedStash
{
    public IStash Stash { get; set; }

    public WaitForAvail()
    {
        Waiting();
    }

    private void Waiting()
    {
        Receive<ServerUp>(_ => BecomeAvailable());
        ReceiveAny(_ => Stash.Stash());
    }

    private void Available()
    {
        Receive<DoWork>(DoWorkHandler);
    }
    
    private void BecomeAvailable()
    {
        Become(Available);
        Stash.UnstashAll();
    }

    private void DoWorkHandler(DoWork message)
    {
        // does some shit
    }
}
`;