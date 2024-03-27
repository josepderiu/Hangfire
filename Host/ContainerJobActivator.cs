using Hangfire;

public class ContainerJobActivator : JobActivator
{
    private readonly IServiceProvider _serviceProvider;

    public ContainerJobActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type jobType)
    {
        return _serviceProvider.GetRequiredService(jobType);
    }
}