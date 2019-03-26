export const receiveActor = `
public class WorkerActor : ReceiveActor
{
    public WorkerActor()
    {
        Receive<string>(StringHandler);
        ReceiveAny(DefaultHandler);
    }

    private void StringHandler(string message)
    {
        Console.WriteLine($"Hello { message }");
    }

    private void DefaultHandler(object _)
    {
        console.WriteLine("Hello World");
    }
}
`;