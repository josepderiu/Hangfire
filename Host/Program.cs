using Hangfire;

using Host.Jobs;
using Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config =>
{
    config.UseInMemoryStorage();
});

builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 1;
    options.SchedulePollingInterval = TimeSpan.FromSeconds(30);
    options.CancellationCheckInterval = TimeSpan.FromSeconds(5);
});

builder.Services.AddHostedService<LowPeriodicityJob>();

builder.Services.AddSingleton<IFooService, FooService>();

var app = builder.Build();

GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(app.Services));

app.UseHangfireDashboard();

var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Enqueued job"));

BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("continuing the enqueued job..."));

BackgroundJob.Schedule(() => Console.WriteLine("Scheduled job"), TimeSpan.FromSeconds(30));

RecurringJob.AddOrUpdate(JobIds.MinutelyRecurringJob, () => Console.WriteLine($"Minutely job: {DateTime.Now}"), Cron.Minutely);

RecurringJob.AddOrUpdate<TypedRecurringJob>(JobIds.TypedRecurringJob, x =>
    x.Execute(CancellationToken.None), Cron.Minutely);

app.Run();