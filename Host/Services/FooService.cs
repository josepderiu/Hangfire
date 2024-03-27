namespace Host.Services;

public class FooService : IFooService
{
    public Task DoWork(CancellationToken cancellationToken)
    {
        Console.WriteLine("FooService: doing some hard work...");

        return Task.CompletedTask;
    }
}
