namespace Host.Services;

public interface IFooService
{
    Task DoWork(CancellationToken cancellationToken = default);
}
