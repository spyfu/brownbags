export const asyncActor = `
public class AsyncActor : ReceiveActor
{
    public AsyncActor()
    {
        ReceiveAsync<BlockingMessage>(BlockingMessageHandler);
        Receive<NonBlockingMessage>(NonBlockingMessageHandler);
    }

    private async Task BlockingMessageHandler(BlockingMessage message)
    {
        var step1 = await longRunningTask1(message.data);
        var response = await step1.longCalc();
        Sender.Tell(response);
    }

    private void NonBlockingMessageHandler(NonBlockingMessage message)
    {
        longRunningTask1(message.data)
            .ContinueWith(async (step1) => {
                return await step1.longCalc();
            })
            .PipeTo(Sender);
    }
}
`;