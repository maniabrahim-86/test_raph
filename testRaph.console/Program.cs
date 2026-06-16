using Microsoft.Extensions.DependencyInjection;
using testRaph.core.services;

internal class Program
{
    private static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IMessengerService, MessengerService>();
        services.AddSingleton<IDashboardService, DashboardService>();

        var provider = services.BuildServiceProvider();

        var dashboard = provider.GetRequiredService<IDashboardService>();
        var messenger = provider.GetRequiredService<IMessengerService>();

        using var cts = new CancellationTokenSource();

        _ = dashboard.StartAsync(cts.Token);

        Console.WriteLine("Press Enter to publish a message");//pour simuler un événement de publication de message
        Console.ReadLine();
        
        
        messenger.PublishMessage();
        
        Console.ReadLine();
        cts.Cancel();

        
        Console.WriteLine("Hello, World!");
    }
}