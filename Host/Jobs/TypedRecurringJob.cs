namespace Host.Jobs;

using Host.Services;

public class TypedRecurringJob(IFooService fooService)
{
    private readonly IFooService _fooService = fooService;

    public async Task Execute(CancellationToken cancellationToken)
    {
        await _fooService.DoWork(cancellationToken);
    }
}
