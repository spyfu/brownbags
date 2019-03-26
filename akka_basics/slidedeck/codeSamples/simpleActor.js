export const simpleActor = `
public class HelloActor : UntypedActor
{
    private override void OnReceive(object message)
    {
        switch (message)
        {
            case Name:
                Console.WriteLine($"Hello {Name}");
                break;
            default:
                Console.WriteLine("Hello World);
                break;
        }
    }
}
`;