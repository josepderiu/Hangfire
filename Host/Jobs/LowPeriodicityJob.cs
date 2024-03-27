namespace Host.Jobs;

using Hangfire;

public class LowPeriodicityJob : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine($"Recurring job: {DateTime.Now}"));

            await Task.Delay(5000, stoppingToken);
        }
    }
}